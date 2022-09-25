# SwissKnife  
  
namespace: swissknife

Instructions: clone repo and run dotnet test to see the results  
  
## Available Tools
| caller | type | Method Name | Description | return |
|--------|------|-------------|-------------|--------|
| any string | Extension Method | ToCamelCase(this string str) | Convert any sentence into camelCase | camelCase String |
| any string | Extension Method | EncryptAES256(this string source, string password, string salt) | Encrypt any string | Encrypted String |
| any encrypted string | Extension Method | DecryptAES256(this string encryptedResult, string password, string salt) | Decrypt an Encrypted String | Decrypted String |
| any string | Extension Method | CreateHash(this string password, string salt) | Create a unique hash | hash string |
| any string | Extension Method | EncodeTo64(this string toEncode) | perform Base 64 encoding | Base64 encoded string |
| any encoded string | Extension Method | DecodeFrom64(this string encodedData) | perform Base 64 decoding | Base64 decoded string |
| RandomPassword | static Method | Generate(int length) | Generates a Random Password | string with (length) chars |
| TokenTools | static Method | CreateAccessToken(string username, string secretKey, string salt, string ip, string userAgent, int minutesValid = 10) | Generates a JWT Access Token | jwt access string token |
| TokenTools | static Method | GetHashedPassword(string password, string salt) | Creates a unique hash based on 2 strings | hash string |
| Token Tools | static Method | DecodeAccessTokenAndSeparateParts(string token) | Splits a JWT Access Token in 3 parts (hash, username, time) | string[] |
| Token Tools | static Method | IsValidAccessToken(string access_token, string ip, string userAgent, string secretKey, string salt) | Check if an access token is valid | bool |
| Token Tools | static Method | CreateIdToken(ClaimsIdentity user, string secretKey, string issuer, int hoursToExpire = 1, SecurityTokenDescriptor tokenDescriptor = null) | Creates an ID JWT Token | jwt identity string token |
| Token Tools | static Method | ValidateIdToken(string token, string secretKey, string validIssuer, bool validateIssuer = true, bool validateAudience = false, TokenValidationParameters tokenValidationParameters = null) | Validates an Identity JWT Token | bool |
| Token Tools | static Method | ExtractPrincipalFromIdToken(string idToken, string secretKey, string validIssuer, bool validateIssuer = true, bool validateAudience = false, TokenValidationParameters tokenValidationParameters = null) | Extracts an Identity from ID Token | ClaimsPrincipal |
| SecretsHandlerService | object | GetFromConfig(string key) | access configuration.GetSection(key) | string |
| SecretsHandlerService | static Method | GetFromEnv(string key) | returns a key from the Environment | string |
| SecretsHandlerService | object | GetObject<T>(string key) where T : class | instantiates a class of type T from Config | T |
