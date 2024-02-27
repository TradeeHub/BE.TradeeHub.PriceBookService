using BE.TradeeHub.PriceBookService.Domain.Interfaces;
using HotChocolate.Execution;
using HotChocolate.Execution.Processing;
using HotChocolate.Language;
using MongoDB.Bson;

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
    
    
    public static BsonDocument ToBsonDocumentProjection(this IReadOnlyCollection<ISelection> selections)
    {
        var projection = new BsonDocument();

        foreach (var selection in selections)
        {
            if (selection.SyntaxNode is not { } topLevelFieldNode || topLevelFieldNode.SelectionSet == null) continue;

            foreach (var subfield in topLevelFieldNode.SelectionSet.Selections.OfType<FieldNode>())
            {
                AddFieldToProjection(subfield, projection);
            }
        }

        return projection;
    }

    private static void AddFieldToProjection(FieldNode node, BsonDocument projection, string? lastValue = null)
    {
        var fieldName = node.Name.Value;
        var capitalizedFieldName = char.ToUpperInvariant(fieldName[0]) + fieldName[1..];

        if (node.SelectionSet == null)
        {
            if (lastValue != null)
            {
                projection.Add(lastValue + "." + capitalizedFieldName, 1);
            }
            else
            {
                projection.Add(capitalizedFieldName, 1);
            }
        }
        else
        {
            foreach (var innerSelectionNode in node.SelectionSet.Selections)
            {
                if (innerSelectionNode is not FieldNode innerFieldNode) continue;

                if (lastValue != null)
                {
                    AddFieldToProjection(innerFieldNode, projection, lastValue + "." + capitalizedFieldName);
                }
                else
                {
                    AddFieldToProjection(innerFieldNode, projection, capitalizedFieldName);
                }
            }
        }
    }
}