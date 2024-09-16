using InventoryManagementSystem.Core.Interfaces.Repositories.AllSharedIRepository;
using InventoryManagementSystem.Infrastructure.Repositories.AllSharedRepository;

namespace InventoryManagementSystem.API.Middlewares
{
    public class TransactionMiddleware
    {
        private readonly RequestDelegate _next;
        public TransactionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, IUnitOfWork unitOfWork)
        {
            try
            {
                // Begin the transaction before processing the request
                unitOfWork.BeginTransaction();

                // Call the next middleware in the pipeline
                await _next(context);

                // Commit the transaction if the request is successful
                await unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                // Rollback the transaction if an error occurs
                await unitOfWork.RollbackTransactionAsync();

                // Log or handle the exception as needed
                throw;
            }
            finally
            {
                // Dispose of the unit of work at the end of the request
                unitOfWork.Dispose();
            }
        }
    }
}
