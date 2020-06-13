using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Presentation;

namespace MediaTinLanh.Control
{
    public class Control_Presentation
    {
        public static void CreateFiles(string location, string Content, string[] format, Stream img)
        {
            string font = format[0];
            string size = format[1];
            string style = format[2];
            //Kiểm tra nội dung nhập vào
            //Content = Control_Util.RemoveSpecialCharacters(Content);
            //Tách đoạn cho nội dung
            string[] sentences = Control_Util.StringSplit(Content);
            //Tạo file
            IPresentation powerpointDoc = Presentation.Create();
            //Tạo slide đầu tiên
            CreateSlide1(powerpointDoc, sentences[0]);
            for (int i = 1; i < sentences.Length; i++)
            {
                //Chèn dữ liệu vào slide
                CreateSlide2 (i-1, powerpointDoc, sentences[i], font, size, style);
            }
            if (img != Stream.Null)
            {
                ILayoutSlide layoutSlide = powerpointDoc.Masters[0].LayoutSlides.Add(SlideLayoutType.Blank, "CustomLayout");
                //Set background of the layout slide
                layoutSlide.Background.Fill.SolidFill.Color = ColorObject.FromArgb(78, 89, 90);
                //Get the stream of an image
                Stream pictureStream = img;
                //Add the picture into layout slide
                layoutSlide.Background.Fill.SolidFill.Color = ColorObject.FromArgb(78, 89, 90);
                 //Add a slide of new designed custom layout to the presentation
                 ISlide slide = powerpointDoc.Slides.Add(layoutSlide);
            }
            //Lưu tệp tin lại
            powerpointDoc.Save(location);

            //Đóng quá trình tạo
            powerpointDoc.Close();
        }

        #region  Slide đầu tiên

        public static void CreateSlide1(IPresentation presentation, string content)
        {
            ISlide firstSlide = presentation.Slides.Add(SlideLayoutType.Blank);
            IShape textShape = firstSlide.AddTextBox(100, 75, 756, 200);
            IParagraph paragraph = textShape.TextBody.AddParagraph();
            
            //Set the horizontal alignment of paragraph
            paragraph.HorizontalAlignment = HorizontalAlignmentType.Center;

            //Adds a textPart in the paragraph
            ITextPart textPart = paragraph.AddTextPart(content);

          
            //Applies font formatting to the text
            textPart.Font.FontSize = 80;
            textPart.Font.Bold = true;
        }

        #endregion

        #region Cac slide tiep theo

        public static void CreateSlide2(int index, IPresentation presentation, string Content, string font, string size, string style)
        {
            ISlide Slide = presentation.Slides.Add(SlideLayoutType.Blank);
            IShape textShape = Slide.AddTextBox(100, 75, 756, 200);
            IParagraph paragraph = textShape.TextBody.AddParagraph();

            //Set the horizontal alignment of paragraph
            paragraph.HorizontalAlignment = HorizontalAlignmentType.Center;

            //Adds a textPart in the paragraph
            ITextPart textPart = paragraph.AddTextPart(Content);

            //Applies font formatting to the text
            textPart.Font.FontName = font;
            textPart.Font.FontSize = int.Parse(size);
            textPart.Font.Bold = true;
        }
        #endregion

        #region Theme

        public static void CreateTheme(IPresentation pptxDoc, Stream pictureStream, string LayoutSlideName, string location)
        {
            //Add a new LayoutSlide to the PowerPoint file
            ILayoutSlide layoutSlide = pptxDoc.Masters[0].LayoutSlides.Add(SlideLayoutType.Blank, LayoutSlideName);

            //Add a shape to the LayoutSlide
            IShape shape = layoutSlide.Shapes.AddShape(AutoShapeType.Diamond, 30, 20, 400, 300);

            //Change the background color for LayoutSlide
            layoutSlide.Background.Fill.SolidFill.Color = ColorObject.FromArgb(78, 89, 90);
            layoutSlide.Shapes.AddPicture(pictureStream, 100, 100, 100, 100);
            
            //Add a slide of new designed custom layout to the presentation
            ISlide slide = pptxDoc.Slides.Add(layoutSlide);
            //Save the PowerPoint file
            pptxDoc.Save(location);

            //Close the Presentation instance
            pptxDoc.Close();
        }
        

        #endregion

    }
}
