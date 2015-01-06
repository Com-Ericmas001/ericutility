using System;
using System.IO;
using System.Windows;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;

namespace Com.Ericmas001.Data.OpenWord.Runs
{
    public class OpenWordRunImage : OpenWordRun
    {
        private readonly byte[] m_Image;
        private readonly Size m_Size;
        public OpenWordRunImage(byte[] image, Size size)
        {
            m_Image = image;
            m_Size = size;
        }

        public override Run ObtainRun(WordprocessingDocument package)
        {
            ImagePart imagePart = package.MainDocumentPart.AddImagePart(ImagePartType.Bmp);
            imagePart.FeedData(new MemoryStream(m_Image));
            return new Run(CreateImage(package.MainDocumentPart.GetIdOfPart(imagePart), m_Size));
        }

        //http://stackoverflow.com/questions/19384826/creating-an-imagepart-isnt-saving-the-relationship-in-openxml
        public Drawing CreateImage(string relationshipId, Size size)
        {
            // Define the reference of the image.
            return new Drawing(
                new DW.Inline(
                    new DW.Extent()

                    {
                        Cx = Convert.ToInt64(size.Width) * 9525L,
                        Cy = Convert.ToInt64(size.Height) * 9525L
                    },
                    new DW.EffectExtent()
                    {
                        LeftEdge = 0L,
                        TopEdge = 0L,
                        RightEdge = 0L,
                        BottomEdge = 0L
                    },
                    new DW.DocProperties()
                    {
                        Id = (UInt32Value)1u,
                        Name = "Picture1"
                    },
                    new DW.NonVisualGraphicFrameDrawingProperties(
                        new A.GraphicFrameLocks()
                        {
                            NoChangeAspect = true
                        }
                    ),
                    new A.Graphic(
                        new A.GraphicData(
                            new PIC.Picture(
                                new PIC.NonVisualPictureProperties(
                                    new PIC.NonVisualDrawingProperties()
                                    {
                                        Id = 0u,
                                        Name = "Koala.jpg"
                                    },
                                    new PIC.NonVisualPictureDrawingProperties()
                                ),
                                new PIC.BlipFill(
                                    new A.Blip(
                                        new A.BlipExtensionList(
                                            new A.BlipExtension()
                                            {
                                                Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                            }
                                        )
                                    )
                                    {
                                        Embed = relationshipId,
                                        CompressionState = A.BlipCompressionValues.Print
                                    },
                                    new A.Stretch(new A.FillRectangle())), new PIC.ShapeProperties(new A.Transform2D(new A.Offset
                                    {
                                        X = 0L,
                                        Y = 0L
                                    },
                                    new A.Extents
                                    {
                                        Cx = Convert.ToInt64(size.Width) * 9525L,
                                        Cy = Convert.ToInt64(size.Height) * 9525L
                                    }
                                ),
                                new A.PresetGeometry(
                                    new A.AdjustValueList())
                                {
                                    Preset = A.ShapeTypeValues.Rectangle
                                }
                                )
                            )
                        )
                        {
                            Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture"
                        }
                    )
                )
                {
                    DistanceFromTop = 0u,
                    DistanceFromBottom = 0u,
                    DistanceFromLeft = 0u,
                    DistanceFromRight = 0u
                }
            );
        }
    }
}
