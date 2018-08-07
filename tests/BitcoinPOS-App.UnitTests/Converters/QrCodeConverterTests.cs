using System;
using System.IO;
using BitcoinPOS_App.Converters;
using BitcoinPOS_App.Models;
using Xamarin.Forms;
using Xunit;

namespace BitcoinPOS_App.UnitTests.Converters
{
    public class QrCodeConverterTests
    {
        [Fact]
        public void CanConvertAddressIntoImage()
        {
            var qcc = new QrCodeConverter();

            var result = qcc.Convert(FakeData.ValidPayment, typeof(ImageSource), null, null);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<ImageSource>(result);
            var img = Assert.IsType<StreamImageSource>(result);

            var ms = img.Stream(default).Result;
            Assert.NotNull(ms);
            Assert.IsType<MemoryStream>(ms);
        }

        [Fact]
        public void ShouldValidateValueAndTargetType()
        {
            var qcc = new QrCodeConverter();

            Assert.Throws<ArgumentNullException>(() => qcc.Convert(null, typeof(ImageSource), null, null));
            Assert.Throws<ArgumentNullException>(() => qcc.Convert("", null, null, null));

            var ex = Assert.Throws<ArgumentException>(() =>
                qcc.Convert(FakeData.ValidPayment, typeof(Payment), null, null));
            Assert.StartsWith("Invalid target type", ex.Message);

            ex = Assert.Throws<ArgumentException>(() => qcc.Convert("", typeof(ImageSource), null, null));
            Assert.StartsWith("Invalid value type", ex.Message);

            ex = Assert.Throws<ArgumentException>(() =>
                qcc.Convert(FakeData.InvalidPaymentWoAddress, typeof(ImageSource), null, null));
            Assert.StartsWith("Invalid payment", ex.Message);
            ex = Assert.Throws<ArgumentException>(() =>
                qcc.Convert(FakeData.InvalidPaymentWoExchangeRate, typeof(ImageSource), null, null));
            Assert.StartsWith("Invalid payment", ex.Message);
        }

        [Fact]
        public void ShouldThrowWhenTryingToConvertBack()
        {
            var qcc = new QrCodeConverter();
            Assert.Throws<InvalidOperationException>(() => qcc.ConvertBack(null, null, null, null));
        }
    }
}
