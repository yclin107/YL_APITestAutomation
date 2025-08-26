using System.Text;

namespace API.Core.Services.OpenAPI
{
    /// <summary>
    /// Generates test class structure (headers, footers, using statements)
    /// </summary>
    public static class TestClassGenerator
    {
        public static string GenerateClassHeader(string className, string tag)
        {
            var codeBuilder = new StringBuilder();
            
            // Using statements
            codeBuilder.AppendLine("using Allure.Net.Commons;");
            codeBuilder.AppendLine("using Allure.NUnit.Attributes;");
            codeBuilder.AppendLine("using NJsonSchema.Validation;");
            codeBuilder.AppendLine("using System.Text.Json;");
            codeBuilder.AppendLine("using static RestAssured.Dsl;");
            codeBuilder.AppendLine();
            
            // Namespace and class declaration
            codeBuilder.AppendLine("namespace API.TestBase.Tests.Component.Generated");
            codeBuilder.AppendLine("{");
            codeBuilder.AppendLine("    [TestFixture]");
            codeBuilder.AppendLine($"    [AllureFeature(\"{tag} API Tests\")]");
            codeBuilder.AppendLine($"    [Category(\"Generated\")]");
            codeBuilder.AppendLine($"    public class {className} : TestBase");
            codeBuilder.AppendLine("    {");
            
            return codeBuilder.ToString();
        }
        
        public static string GenerateClassFooter()
        {
            var codeBuilder = new StringBuilder();
            
            codeBuilder.AppendLine("    }");
            codeBuilder.AppendLine("}");
            
            return codeBuilder.ToString();
        }
    }
}