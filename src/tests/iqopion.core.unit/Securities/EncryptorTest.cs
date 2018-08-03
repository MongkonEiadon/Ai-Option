using Xunit;

namespace iqopion.core.unit.Securities {
    [Trait("Category", "Encrypt")]
    public class EncryptorTest {
        [Fact]
        public void Encode_WithExistingString_EncodeShouldWork() {
            //arrange
            var plainText = "AnyText";
        }
    }
}