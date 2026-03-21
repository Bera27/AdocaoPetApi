using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AdocaoPetApi.Extensions
{
    public static class ModelStateExtension
    {
        // Recebe todas as mensagens dos erros do ModelState e retorna o resultado
        public static List<string> GetErrors(this ModelStateDictionary modelState)
        {
            var result = new List<string>();
            foreach (var item in modelState.Values)
                result.AddRange(item.Errors.Select(error => error.ErrorMessage));

            return result;
        }
    }
}