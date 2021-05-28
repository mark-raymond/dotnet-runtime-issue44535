# dotnet-runtime-issue44535
Repro of https://github.com/dotnet/runtime/issues/44535

Steps:

1. Clone this repo
2. In the root of the clone, run `dotnet run --project CertIssueRepro/CertIssueRepro.csproj`

On Windows, this succeeds and prints the certificate fingerprint. On Linux, it results in an exception:

```
Unhandled exception. System.Security.Cryptography.CryptographicException: The certificate data cannot be read with the provided password, the password may be incorrect.
 ---> System.Security.Cryptography.CryptographicException: A certificate referenced a private key which was already referenced, or could not be loaded.
   at Internal.Cryptography.Pal.UnixPkcs12Reader.BuildCertsWithKeys(CertBagAsn[] certBags, AttributeAsn[][] certBagAttrs, CertAndKey[] certs, Int32 certBagIdx, SafeBagAsn[] keyBags, RentedSubjectPublicKeyInfo[] publicKeyInfos, AsymmetricAlgorithm[] keys, Int32 keyBagIdx)
   at Internal.Cryptography.Pal.UnixPkcs12Reader.Decrypt(ReadOnlySpan`1 password, ReadOnlyMemory`1 authSafeContents)
   at Internal.Cryptography.Pal.UnixPkcs12Reader.VerifyAndDecrypt(ReadOnlySpan`1 password, ReadOnlyMemory`1 authSafeContents)
   at Internal.Cryptography.Pal.UnixPkcs12Reader.Decrypt(SafePasswordHandle password)
   --- End of inner exception stack trace ---
   at Internal.Cryptography.Pal.UnixPkcs12Reader.Decrypt(SafePasswordHandle password)
   at Internal.Cryptography.Pal.PkcsFormatReader.TryReadPkcs12(OpenSslPkcs12Reader pfx, SafePasswordHandle password, Boolean single, ICertificatePal& readPal, List`1& readCerts)
   at Internal.Cryptography.Pal.PkcsFormatReader.TryReadPkcs12(ReadOnlySpan`1 rawData, SafePasswordHandle password, Boolean single, ICertificatePal& readPal, List`1& readCerts, Exception& openSslException)
   at Internal.Cryptography.Pal.OpenSslX509CertificateReader.FromFile(String fileName, SafePasswordHandle password, X509KeyStorageFlags keyStorageFlags)
   at System.Security.Cryptography.X509Certificates.X509Certificate..ctor(String fileName, String password, X509KeyStorageFlags keyStorageFlags)
   at System.Security.Cryptography.X509Certificates.X509Certificate2..ctor(String fileName, String password)
   at CertIssueRepro.Program.Main() in /app/CertIssueRepro/Program.cs:line 12
```
