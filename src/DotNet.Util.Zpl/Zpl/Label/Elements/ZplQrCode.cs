﻿using System.Collections.Generic;

namespace DotNet.Util.Zpl.Label.Elements
{
    /// <summary>
    /// ZplQrCode
    /// </summary>
    public class ZplQrCode : ZplPositionedElementBase
    {
        /// <summary>
        /// Content
        /// </summary>
        public string Content { get; protected set; }
        /// <summary>
        /// Model
        /// </summary>
        public int Model { get; private set; }
        /// <summary>
        /// MagnificationFactor
        /// </summary>
        public int MagnificationFactor { get; private set; }
        /// <summary>
        /// ErrorCorrectionLevel
        /// </summary>
        public ErrorCorrectionLevel ErrorCorrectionLevel { get; private set; }
        /// <summary>
        /// MaskValue
        /// </summary>
        public int MaskValue { get; private set; }

        /// <summary>
        /// Zpl QrCode
        /// </summary>
        /// <param name="content"></param>
        /// <param name="positionX"></param>
        /// <param name="positionY"></param>
        /// <param name="model">1 (original) and 2 (enhanced – recommended)</param>
        /// <param name="magnificationFactor">Size of the QR code, 1 on 150 dpi printers, 2 on 200 dpi printers, 3 on 300 dpi printers, 6 on 600 dpi printers</param>
        /// <param name="errorCorrectionLevel"></param>
        /// <param name="maskValue">0-7, (default: 7)</param>
        public ZplQrCode(
            string content,
            int positionX,
            int positionY,
            int model = 2,
            int magnificationFactor = 2,
            ErrorCorrectionLevel errorCorrectionLevel = ErrorCorrectionLevel.HighReliability,
            int maskValue = 7)
            : base(positionX, positionY)
        {
            Content = content;
            Model = model;
            MagnificationFactor = magnificationFactor;
            ErrorCorrectionLevel = errorCorrectionLevel;
            MaskValue = maskValue;
        }

        ///<inheritdoc/>
        public override IEnumerable<string> Render(ZplRenderOptions context)
        {
            //^ FO100,100
            //^ BQN,2,10
            //^ FDMM,AAC - 42 ^ FS
            var result = new List<string>();
            result.AddRange(Origin.Render(context));
            result.Add($"^BQN,{Model},{context.Scale(MagnificationFactor)},{RenderErrorCorrectionLevel(ErrorCorrectionLevel)},{MaskValue}");
            result.Add($"^FD{RenderErrorCorrectionLevel(ErrorCorrectionLevel)}A,{Content}^FS");

            return result;
        }
    }
}
