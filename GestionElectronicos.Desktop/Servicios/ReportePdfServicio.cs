
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Windows.Forms.VisualStyles;
using GestionElectronicos.Desktop.Modelos;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using Document = MigraDoc.DocumentObjectModel.Document;
using Section = MigraDoc.DocumentObjectModel.Section;
using Orientation = MigraDoc.DocumentObjectModel.Orientation;
using Color = MigraDoc.DocumentObjectModel.Color;
using VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment;
using static System.Collections.Specialized.BitVector32;

namespace GestionElectronicos.Desktop.Servicios;

public static class ReportePdfServicio
{
    public static void Generar(
        List<VentasPorProducto> ventas,
        List<StockReporte> stock,
        string filtro)
    {
        var documento = new Document();
        documento.Info.Title = "Reporte de ventas e inventario";
        documento.Info.Author = "Sistema de Gestión de Ventas";

        var normal = documento.Styles["Normal"];
        normal.Font.Name = "Arial";
        normal.Font.Size = 9;

        var seccion = documento.AddSection();
        seccion.PageSetup.PageFormat = PageFormat.A4;
        seccion.PageSetup.Orientation = Orientation.Portrait;
        seccion.PageSetup.TopMargin = Unit.FromCentimeter(2);
        seccion.PageSetup.BottomMargin = Unit.FromCentimeter(2);
        seccion.PageSetup.LeftMargin = Unit.FromCentimeter(2);
        seccion.PageSetup.RightMargin = Unit.FromCentimeter(2);

        // Encabezado del reporte.
        var marca = seccion.AddParagraph("GESTIÓN DE VENTAS");
        marca.Format.Font.Size = 15;
        marca.Format.Font.Bold = true;
        marca.Format.Font.Color = Color.FromRgb(24, 54, 93);
        marca.Format.SpaceAfter = Unit.FromCentimeter(0.3);

        var titulo = seccion.AddParagraph("Reporte de ventas y existencias");
        titulo.Format.Font.Size = 13;
        titulo.Format.Font.Bold = true;
        titulo.Format.SpaceAfter = Unit.FromCentimeter(0.3);

        seccion.AddParagraph(
            $"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}");
        seccion.AddParagraph($"Filtro aplicado: {filtro}");
        seccion.AddParagraph(" ");

        // Reporte de productos vendidos.
        AgregarTitulo(seccion, "1. PRODUCTOS VENDIDOS");

        var tablaVentas = CrearTabla(seccion, 3);
        Encabezado(tablaVentas, "Producto", "Unidades", "Monto");

        foreach (var venta in ventas)
        {
            Fila(tablaVentas,
                venta.Producto,
                venta.Unidades.ToString("N0"),
                $"$ {venta.Monto:N2}");
        }

        if (ventas.Count == 0)
            Fila(tablaVentas, "Sin ventas registradas", "-", "-");

        seccion.AddParagraph(" ");

        var resumenVentas = seccion.AddParagraph(
            $"Total de unidades vendidas: {ventas.Sum(x => x.Unidades):N0}" +
            $"     Monto acumulado: $ {ventas.Sum(x => x.Monto):N2}");

        resumenVentas.Format.Font.Bold = true;
        resumenVentas.Format.SpaceAfter =
            Unit.FromCentimeter(0.6);

        AgregarTitulo(seccion, "2. EXISTENCIAS DE INVENTARIO");

        var tablaStock = CrearTabla(seccion, 4);
        Encabezado(tablaStock,
            "Producto", "Precio", "Stock", "Estado");

        foreach (var producto in stock)
        {
            var fila = Fila(tablaStock,
                producto.Producto,
                $"$ {producto.Precio:N2}",
                producto.Stock.ToString("N0"),
                producto.Stock <= 5 ? "CRÍTICO" :
                producto.Stock <= 10 ? "PRECAUCIÓN" :
                "NORMAL");

            fila.Shading.Color = producto.Stock <= 5
                ? Color.FromRgb(255, 220, 220)
                : producto.Stock <= 10
                    ? Color.FromRgb(255, 245, 200)
                    : Color.FromRgb(225, 245, 230);
        }

        if (stock.Count == 0)
            Fila(tablaStock, "Sin existencias registradas", "-", "-", "-");

        seccion.AddParagraph(" ");

        var resumenStock = seccion.AddParagraph(
            $"Productos consultados: {stock.Count}" +
            $"     Stock crítico: {stock.Count(x => x.Stock <= 5)}");

        resumenStock.Format.Font.Bold = true;

        var pie = seccion.Footers.Primary.AddParagraph();
        pie.AddText("Sistema de Gestión de Productos Electrónicos  |  Página ");
        pie.AddPageField();
        pie.Format.Font.Size = 8;
        pie.Format.Font.Color = Colors.Gray;
        pie.Format.Alignment = ParagraphAlignment.Center;

        string ruta = Path.Combine(
            Path.GetTempPath(),
            $"Reporte_Electronicos_{DateTime.Now:yyyyMMdd_HHmmss_fff}.pdf");

        var renderer = new PdfDocumentRenderer
        {
            Document = documento
        };

        renderer.RenderDocument();
        renderer.PdfDocument.Save(ruta);

        Process.Start(new ProcessStartInfo
        {
            FileName = ruta,
            UseShellExecute = true
        });
    }

    private static void AgregarTitulo(
        MigraDoc.DocumentObjectModel.Section seccion, string texto)
    {
        var parrafo = seccion.AddParagraph(texto);
        parrafo.Format.Font.Bold = true;
        parrafo.Format.Font.Size = 11;
        parrafo.Format.Font.Color = Color.FromRgb(24, 54, 93);
        parrafo.Format.SpaceAfter = Unit.FromCentimeter(0.2);
        parrafo.Format.KeepWithNext = true;
    }

    private static Table CrearTabla(
        MigraDoc.DocumentObjectModel.Section seccion, int columnas)
    {
        var tabla = seccion.AddTable();
        tabla.Borders.Width = 0.5;
        tabla.Borders.Color = Colors.LightGray;
        tabla.Rows.LeftIndent = 0;

        double[] anchos = columnas == 3
            ? new[] { 9.0, 3.0, 4.0 }
            : new[] { 7.0, 3.0, 2.0, 4.0 };

        foreach (double ancho in anchos)
            tabla.AddColumn(Unit.FromCentimeter(ancho));

        return tabla;
    }

    private static void Encabezado(
        Table tabla, params string[] valores)
    {
        var fila = Fila(tabla, valores);
        fila.HeadingFormat = true;
        fila.Shading.Color = Color.FromRgb(24, 54, 93);
        fila.Format.Font.Color = Colors.White;
        fila.Format.Font.Bold = true;
    }

    private static Row Fila(
        Table tabla, params string[] valores)
    {
        var fila = tabla.AddRow();
        fila.VerticalAlignment = VerticalAlignment.Center;
        fila.TopPadding = Unit.FromMillimeter(2);
        fila.BottomPadding = Unit.FromMillimeter(2);

        for (int i = 0; i < valores.Length; i++)
        {
            fila.Cells[i].AddParagraph(valores[i]);

            if (i > 0)
                fila.Cells[i].Format.Alignment =
                    ParagraphAlignment.Center;
        }

        return fila;
    }
}
