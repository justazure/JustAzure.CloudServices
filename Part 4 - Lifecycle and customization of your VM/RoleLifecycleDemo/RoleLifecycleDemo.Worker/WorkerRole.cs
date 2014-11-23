using System.Threading;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

using RoleLifecycleDemo.Worker.Model;

namespace RoleLifecycleDemo.Worker
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent _runCompleteEvent = new ManualResetEvent(false);
        private CloudTable _table;

        public override bool OnStart()
        {
            _table = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageAccount"))
                .CreateCloudTableClient()
                .GetTableReference("RoleInfo");
            _table.CreateIfNotExists();

            // Log the OnStart call.
            _table.Execute(TableOperation.Insert(TableLifecycleEvent.New(RoleEnvironment.CurrentRoleInstance.Id, "OnStart()")));

            return base.OnStart();
        }

        public override void Run()
        {
            try
            {
                // Log the Run call.
                _table.Execute(TableOperation.Insert(TableLifecycleEvent.New(RoleEnvironment.CurrentRoleInstance.Id, "Run()")));

                // Start the loop.
                RunAsync(_cancellationTokenSource.Token).Wait();
            }
            finally
            {
                _runCompleteEvent.Set();
            }
        }
        private async Task RunAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                // Log the Run call.
                _table.Execute(TableOperation.Insert(TableLifecycleEvent.New(RoleEnvironment.CurrentRoleInstance.Id, "Loop in Run()")));

                await Task.Delay(1000, cancellationToken);
            }
        }

        public override void OnStop()
        {
            _cancellationTokenSource.Cancel();
            _runCompleteEvent.WaitOne();

            // Log the Run call.
            _table.Execute(TableOperation.Insert(TableLifecycleEvent.New(RoleEnvironment.CurrentRoleInstance.Id, "OnStop()")));

            base.OnStop();
        }
    }
}
