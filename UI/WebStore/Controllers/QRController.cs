using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace WebStore.Controllers;

public class QRController : Controller
{
    public IActionResult Code(string str)
    {
        var generator = new QRCodeGenerator();
        var data = generator.CreateQrCode(str, QRCodeGenerator.ECCLevel.Q);
        var code = new PngByteQRCode(data);

        var image_bytes = code.GetGraphic(20);

        return File(image_bytes, "image/png");
    }
}