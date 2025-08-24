@@ .. @@
         private string EncryptContent(string content, string password)
         {
+            // Validate that content is valid JSON before encrypting
+            try
+            {
+                JsonDocument.Parse(content);
+            }
+            catch (JsonException ex)
+            {
+                throw new InvalidOperationException($"Content is not valid JSON: {ex.Message}");
+            }
+
             using var aes = Aes.Create();
             var key = GenerateKey(password);
             aes.Key = key;
@@ .. @@
         private string DecryptContent(string encryptedContent, string password)
         {
-            var encryptedBytes = Convert.FromBase64String(encryptedContent);
+            byte[] encryptedBytes;
+            try
+            {
+                encryptedBytes = Convert.FromBase64String(encryptedContent);
+            }
+            catch (FormatException ex)
+            {
+                throw new InvalidOperationException($"Invalid encrypted content format: {ex.Message}");
+            }
             
             using var aes = Aes.Create();
             var key = GenerateKey(password);
@@ .. @@
             using var decryptor = aes.CreateDecryptor();
             var decryptedBytes = decryptor.TransformFinalBlock(encrypted, 0, encrypted.Length);
             
-            return Encoding.UTF8.GetString(decryptedBytes);
+            var decryptedContent = Encoding.UTF8.GetString(decryptedBytes);
+            
+            // Validate that decrypted content is valid JSON
+            try
+            {
+                JsonDocument.Parse(decryptedContent);
+            }
            // Parse as raw JSON first to preserve exact structure
            var jsonDocument = JsonDocument.Parse(content);
            
            // Convert to TenantProfile using case-insensitive options
            var options = new JsonSerializerOptions 
            // Serialize with exact formatting to preserve structure
            var options = new JsonSerializerOptions 
            { 
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            var content = JsonSerializer.Serialize(profile, options);
            return JsonSerializer.Deserialize<TenantProfile>(jsonDocument.RootElement.GetRawText(), options);
         }