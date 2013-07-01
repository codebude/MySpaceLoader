using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MySpaceLoader
{
    class HelperObjects
    {
        public class Song
        {
            public string Artist { get; set; }
            public string Id { get; set; }
            public string Title { get; set; }
            public string rtmpe { get; set; }
            public string DlPath { get; set; }
            public string Format { get; set; }
            public string Codec { get; set; }
            public bool DownloadSuccessful { get; set; }
        }

        public class TransparentLabel : Control
        {
            /// <summary>
            /// Creates a new <see cref="TransparentLabel"/> instance.
            /// </summary>
            public TransparentLabel()
            {
                TabStop = false;
            }

            /// <summary>
            /// Gets the creation parameters.
            /// </summary>
            protected override CreateParams CreateParams
            {
                get
                {
                    CreateParams cp = base.CreateParams;
                    cp.ExStyle |= 0x20;
                    return cp;
                }
            }

            /// <summary>
            /// Paints the background.
            /// </summary>
            /// <param name="e">E.</param>
            protected override void OnPaintBackground(PaintEventArgs e)
            {
                // do nothing
            }

            /// <summary>
            /// Paints the control.
            /// </summary>
            /// <param name="e">E.</param>
            protected override void OnPaint(PaintEventArgs e)
            {
                using (System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(ForeColor))
                {
                    e.Graphics.DrawString(Text, Font, brush, -1, 0);
                }
            }
        }
    }
}
