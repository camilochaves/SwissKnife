using SwissKnife;
namespace swissknifetests;

[TestClass]
public partial class TestSwissKnife
{
    [TestMethod]
    [DataRow("camilo cHAVES", "camiloChaves")]
    [DataRow("Anakin_skyWALKER", "anakinSkywalker")]
    public void TestCamelCase(string data, string result)
    {
        Assert.IsTrue(data.ToCamelCase() == result);
    }

    [TestMethod]
    [DataRow("data to encrypt")]
    public void TestEncryptDecrypt(string data)
    {
        var rndPwd = RandomPassword.Generate(10);
        var salt = "Camilo";
        var encryptedData = data.EncryptAES256(rndPwd, salt);
        var decryptedData1 = encryptedData.DecryptAES256(rndPwd, salt);
        var decryptedData2 = encryptedData.DecryptAES256("wrongPassword", salt);
        var decryptedData3 = encryptedData.DecryptAES256(rndPwd, "wrongSalt");

        Assert.IsTrue(decryptedData1 == data);
        Assert.IsTrue(decryptedData2 != data);
        Assert.IsTrue(decryptedData3 != data);
    }

    [TestMethod]
    [DataRow("data to encode")]
    public void TestEncodeDecode64(string data)
    {
        var encryptedData = data.EncodeTo64();
        var decryptedData = encryptedData.DecodeFrom64();

        Assert.IsTrue(decryptedData == data);
    }

    [TestMethod]
    [DataRow("data to hash", "Data to hash")]
    public void CreateHash(string data1, string data2)
    {
        var salt = "Camilo";
        var hash1 = data1.CreateHash(salt);
        var hash2 = data2.CreateHash(salt);
        var hash3 = data1.CreateHash("camilo");

        Assert.IsTrue(hash1 != hash2);
        Assert.IsTrue(hash1 != hash3);
    }
}