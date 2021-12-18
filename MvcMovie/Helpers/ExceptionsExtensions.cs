using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Helpers
{
    public static class ExceptionsExtensions
    {
        public static string GetMessage(this DbUpdateException ex)
        {
            var message = ex.GetBaseException() != null ? ex.GetBaseException().Message : ex.Message;

            if (message.Contains("FOREIGN KEY"))
            {
                message = "Opération impossible car il existe des données liées à cette fiche.";
            }

            return message;
        }
    }
}
