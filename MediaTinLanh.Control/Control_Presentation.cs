using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Presentation;

namespace MediaTinLanh.Control
{
    public class Control_Presentation
    {
        public static void CreateFiles(string location, string Content, string[] format)
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
                CreateSlide2(i, powerpointDoc, sentences[i], font, size, style);
            }

            //Lưu tệp tin lại
            powerpointDoc.Save(location);

            //Đóng quá trình tạo
            powerpointDoc.Close();
        }

        #region  Slide đầu tiên

        public static void CreateSlide1(IPresentation presentation, string content)
        {
            ISlide slide1 = presentation.Slides.Add(SlideLayoutType.TitleOnly);
            IShape shape1 = slide1.Shapes[0] as IShape;
            shape1.Left = 1.5 * 72;
            shape1.Top = 1.94 * 72;
            shape1.Width = 10.32 * 72;
            shape1.Height = 2 * 72;

            ITextBody textFrame1 = shape1.TextBody;
            IParagraphs paragraphs1 = textFrame1.Paragraphs;
            IParagraph paragraph1 = paragraphs1.Add();
            ITextPart textPart1 = paragraph1.AddTextPart();
            paragraphs1[0].IndentLevelNumber = 0;
            textPart1.Text = content;
            textPart1.Font.FontName = "HelveticaNeue LT 65 Medium";
            textPart1.Font.FontSize = 80;
            textPart1.Font.Bold = true;
            slide1.Shapes.RemoveAt(0);
        }

        #endregion

        #region Cac slide tiep theo

        #region Slide2
        public static  void CreateSlide2(int index,IPresentation presentation, string Content, string font, string size, string style)
        {
            ISlide slide2 = presentation.Slides.Add(SlideLayoutType.Title);
            IShape shape1 = slide2.Shapes[0] as IShape;
            ITextBody textFrame1 = shape1.TextBody;

            //Instance to hold paragraphs in textframe
            IParagraphs paragraphs1 = textFrame1.Paragraphs;
            IParagraph paragraph1 = paragraphs1.Add();
            ITextPart textpart1 = paragraph1.AddTextPart();
            paragraphs1[0].HorizontalAlignment = HorizontalAlignmentType.Left;
            textpart1.Text = Content;
            textpart1.Font.FontName = font;
            textpart1.Font.FontSize = Int16.Parse(size);
            slide2.Shapes.RemoveAt(index);
        }
        # endregion

        #endregion

    }
}
