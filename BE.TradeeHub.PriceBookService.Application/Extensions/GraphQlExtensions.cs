using BE.TradeeHub.PriceBookService.Domain.Interfaces;
using HotChocolate.Execution;

namespace BE.TradeeHub.PriceBookService.Application.Extensions;

public static class GraphQlExtensions
{
    public static void ExportGraphQlSchemaToFile(this WebApplication app, string schemaFilePath = "schema.graphql")
    {
        // Check if we are in the 'Development' or 'Docker' environment
        var appSettings = app.Services.GetRequiredService<IAppSettings>();
        if (appSettings.Environment is not ("Development" or "Docker")) return;
        
        var resolver = app.Services.GetRequiredService<IRequestExecutorResolver>();
        var executor = resolver.GetRequestExecutorAsync().Result; // Consider using async pattern if possible

        if (File.Exists(schemaFilePath))
        {
            var oldSchema = File.ReadAllText(schemaFilePath);
            var newSchema = executor.Schema.ToString();
            if (newSchema != oldSchema)
            {
                File.WriteAllText(schemaFilePath, newSchema);
            }
        }
        else
        {
            // If the file does not exist, write the new schema directly
            var newSchema = executor.Schema.ToString();
            File.WriteAllText(schemaFilePath, newSchema);
        }
    }
}