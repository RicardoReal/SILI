using iTextSharp.text;
using iTextSharp.text.pdf;
using SILI.Models;
using System;
using System.IO;

namespace SILI
{
    public class FileGenerator
    {
        public static byte[] GenerateEtiqueta(ProdutoTriagem produtoTriagem)
        {
            Rectangle rectangle = new Rectangle(Utilities.MillimetersToPoints(100), Utilities.MillimetersToPoints(50));
            Document document = new Document(rectangle, 5, 5, 10, 10);
            MemoryStream ms = new MemoryStream();

            PdfWriter writer = PdfWriter.GetInstance(document, ms);

            document.Open();

            // Imagem
            Image image = imageGenerate(produtoTriagem.Triagem.NrProcesso);
            image.ScaleAbsolute(new Rectangle(Utilities.MillimetersToPoints(90), Utilities.MillimetersToPoints(20)));

            document.Add(image);

            document.Add(Chunk.NEWLINE);

            PdfPTable table = new PdfPTable(2);

            Font smallFont = new Font(Font.FontFamily.HELVETICA, 7);

            table.AddCell(new PdfPCell(new Paragraph("EAN", smallFont)));
            table.AddCell(new PdfPCell(new Paragraph(produtoTriagem.Produto.EAN, smallFont)));


            table.AddCell(new PdfPCell(new Paragraph("Lote", smallFont)));
            table.AddCell(new PdfPCell(new Paragraph(produtoTriagem.Lote, smallFont)));

            table.AddCell(new PdfPCell(new Paragraph("Validade", smallFont)));
            table.AddCell(new PdfPCell(new Paragraph(produtoTriagem.Validade.ToString(), smallFont)));

            table.AddCell(new PdfPCell(new Paragraph("Data Triagem", smallFont)));
            table.AddCell(new PdfPCell(new Paragraph(DateTime.Now.ToString(), smallFont)));

            table.AddCell(new PdfPCell(new Paragraph("Triado Por", smallFont)));
            table.AddCell(new PdfPCell(new Paragraph(produtoTriagem.Triagem.User.ToString(), smallFont)));

            document.Add(table);
            document.Close();

            writer.Close();

            ms.Close();

            return ms.ToArray();
        }

        public static byte[] GenerateEtiqueta(DetalheRecepcao detalheRecepcao)
        {
            Rectangle rectangle = new Rectangle(Utilities.MillimetersToPoints(100), Utilities.MillimetersToPoints(50));
            Document document = new Document(rectangle, 5, 5, 10, 10);
            MemoryStream ms = new MemoryStream();

            PdfWriter writer = PdfWriter.GetInstance(document, ms);

            document.Open();

            // Imagem
            Image image = imageGenerate(detalheRecepcao.NrDetalhe);
            image.ScaleAbsolute(new Rectangle(Utilities.MillimetersToPoints(90), Utilities.MillimetersToPoints(20)));

            document.Add(image);

            document.Add(Chunk.NEWLINE);

            PdfPTable table = new PdfPTable(2);

            Font smallFont = new Font(Font.FontFamily.HELVETICA, 7);

            table.AddCell(new PdfPCell(new Paragraph("Cliente", smallFont)));
            table.AddCell(new PdfPCell(new Paragraph(detalheRecepcao.Cliente.FormattedToString, smallFont)));

            table.AddCell(new PdfPCell(new Paragraph("Data", smallFont)));
            table.AddCell(new PdfPCell(new Paragraph(detalheRecepcao.Recepcao.DataHora.ToString(), smallFont)));

            table.AddCell(new PdfPCell(new Paragraph("Tipo Recepçºao", smallFont)));
            table.AddCell(new PdfPCell(new Paragraph(detalheRecepcao.TipoDevolucao.Descricao, smallFont)));

            table.AddCell(new PdfPCell(new Paragraph("Nr. Volumes", smallFont)));
            table.AddCell(new PdfPCell(new Paragraph(detalheRecepcao.NrVolumes.ToString(), smallFont)));

            table.AddCell(new PdfPCell(new Paragraph("NReferência", smallFont)));
            table.AddCell(new PdfPCell(new Paragraph(detalheRecepcao.NReferencia.ToString(), smallFont)));

            table.AddCell(new PdfPCell(new Paragraph("Recepcionado Por", smallFont)));
            table.AddCell(new PdfPCell(new Paragraph(detalheRecepcao.Recepcao.User.FormattedToString, smallFont)));

            document.Add(table);
            document.Close();

            writer.Close();

            ms.Close();

            return ms.ToArray();
        }

        public static byte[] GenerateEtiqueta(EtiquetaMultiRef etiquetaMultiRef)
        {
            Rectangle rectangle = new Rectangle(Utilities.MillimetersToPoints(100), Utilities.MillimetersToPoints(50));
            Document document = new Document(rectangle, 5, 5, 10, 10);
            MemoryStream ms = new MemoryStream();

            PdfWriter writer = PdfWriter.GetInstance(document, ms);

            document.Open();

            Font titleFont = new Font(Font.FontFamily.HELVETICA, 10);

            Paragraph title = new Paragraph(etiquetaMultiRef.Tratamento.Descricao + " - " + etiquetaMultiRef.Tipologia.Descricao);
            title.Alignment = Element.ALIGN_CENTER;
            title.Font = titleFont;
            document.Add(title);

            // Imagem
            Image image = imageGenerate(etiquetaMultiRef.NrDetalhe);
            image.ScaleAbsolute(new Rectangle(Utilities.MillimetersToPoints(90), Utilities.MillimetersToPoints(20)));

            document.Add(image);

            PdfPTable table = new PdfPTable(2);

            document.Add(new Chunk(" "));

            Font smallFont = new Font(Font.FontFamily.HELVETICA, 7);

            table.AddCell(new PdfPCell(new Paragraph("Localização:  ", smallFont)));
            table.AddCell(new PdfPCell(new Paragraph(etiquetaMultiRef.Localizacao, smallFont)));

            document.Add(table);
            document.Close();

            writer.Close();

            ms.Close();

            return ms.ToArray();
        }

        private static Image imageGenerate(string text)
        {
            using (SILI_DBEntities db = new SILI_DBEntities())
            {
                text = text.Replace("-", string.Empty);

                BarcodeLib.Barcode barCode = new BarcodeLib.Barcode(text, BarcodeLib.TYPE.CODE39);
                barCode.IncludeLabel = true;

                System.Drawing.Image image = barCode.Encode(BarcodeLib.TYPE.CODE39, text);
                System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();

                byte[] imageByte = (byte[])converter.ConvertTo(image, typeof(byte[]));

                return Image.GetInstance(imageByte);
            }
        }
    }
}