﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace Epicoil.Library.Frameworks
{
    public class CustomComboBox : System.Windows.Forms.ComboBox
    {
        private Color borderColor = Color.FromArgb(138, 156, 246);
        //Gray
        private Color highlightColor = Color.FromArgb(128, 128, 128);
        //Blue
        private Color hoverColor = Color.Silver; //Silver

        private Size arrowSize;
        private Rectangle arrowRectangle;
        private Rectangle topTextBoxRectangle;
        private Rectangle bottomTextBoxRectangle;
        private ComboBoxState textBoxState = ComboBoxState.Normal;
        private ComboBoxState arrowState = ComboBoxState.Normal;
        private String bottomText = "Using ComboBoxRenderer";
        private bool isActivated = false;
        private const int minHeight = 38;
        private const int minWidth = 40;

        public CustomComboBox()
            : base()
        {
            this.Location = new Point(10, 10);
            this.Size = new Size(140, 38);
            this.Font = SystemFonts.IconTitleFont;
            this.Text = "Click the button";
            this.borderColor = Color.FromArgb(138, 156, 246);

            // Initialize the rectangles to look like the standard combo  
            // box control.
            arrowSize = new Size(18, 20);
            arrowRectangle = new Rectangle(ClientRectangle.X +
                ClientRectangle.Width - arrowSize.Width - 1,
                ClientRectangle.Y + 1,
                arrowSize.Width,
                arrowSize.Height);
            topTextBoxRectangle = new Rectangle(ClientRectangle.X,
                ClientRectangle.Y,
                ClientRectangle.Width,
                arrowSize.Height + 2);
            bottomTextBoxRectangle = new Rectangle(ClientRectangle.X,
                ClientRectangle.Y + topTextBoxRectangle.Height,
                ClientRectangle.Width,
                topTextBoxRectangle.Height - 6);
        }

        // Draw the combo box in the current state. 
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!ComboBoxRenderer.IsSupported)
            {
                this.Parent.Text = "Visual Styles Disabled";
                return;
            }

            this.Parent.Text = "CustomComboBox Enabled";

            // Always draw the main text box and drop down arrow in their  
            // current states
            ComboBoxRenderer.DrawTextBox(e.Graphics, topTextBoxRectangle,
                this.Text, this.Font, textBoxState);
            ComboBoxRenderer.DrawDropDownButton(e.Graphics, arrowRectangle,
                arrowState);

            // Only draw the bottom text box if the arrow has been clicked 
            if (isActivated)
            {
                ComboBoxRenderer.DrawTextBox(e.Graphics,
                    bottomTextBoxRectangle, bottomText, this.Font,
                    textBoxState);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            // Check whether the user clicked the arrow. 
            if (arrowRectangle.Contains(e.Location) &&
                ComboBoxRenderer.IsSupported)
            {
                // Draw the arrow in the pressed state.
                arrowState = ComboBoxState.Pressed;

                // The user has activated the combo box. 
                if (!isActivated)
                {
                    this.Text = "Clicked!";
                    textBoxState = ComboBoxState.Pressed;
                    isActivated = true;
                }

                // The user has deactivated the combo box. 
                else
                {
                    this.Text = "Click here";
                    textBoxState = ComboBoxState.Normal;
                    isActivated = false;
                }

                // Redraw the control.
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (arrowRectangle.Contains(e.Location) &&
                ComboBoxRenderer.IsSupported)
            {
                arrowState = ComboBoxState.Normal;
                Invalidate();
            }
        }
    }

}


