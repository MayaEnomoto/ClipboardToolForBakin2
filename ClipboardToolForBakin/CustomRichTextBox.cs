using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClipboardToolForBakin2
{
    public class CustomRichTextBox : RichTextBox
    {
        bool isSmallSize = false;
        Stack<string> smallSizeText = new Stack<string>();

        public CustomRichTextBox()
        {
            this.ForeColor = Color.Black;
            this.Font = new Font(this.Font.FontFamily, 12);
        }

        public void ChangeTransparent(bool flg)
        {
            if (flg == true)
            {
                this.BackColor = SystemColors.GradientInactiveCaption;
            }
            else
            {
                this.BackColor = SystemColors.Info;
            }
        }

        public async Task StreamText(string text, int delay, bool autoScroll, CancellationToken cancelToken, CancellationToken skipToken, string instantText = null)
        {
            DateTime lastScrollTime = DateTime.Now;
            float currentSize = this.Font.Size;
            bool isBold = false, isItalic = false, isUnderline = false;
            Stack<char> decorations = new Stack<char>();

            if (!string.IsNullOrEmpty(instantText))
            {
                this.Invoke(new Action(() => this.AppendText(instantText)));
            }

            string replacedText = text.Replace("\r\n", "\n");

            bool isInstant = false;
            int charCount = 0;
            float currentDelay = delay;
            Color currentColor = this.SelectionColor;
            for (; charCount < replacedText.Length; charCount++)
            {
                if (cancelToken.IsCancellationRequested) break;
                if (skipToken.IsCancellationRequested) delay = 0;
                try
                {
                    // Check for \>
                    if (charCount < replacedText.Length - 1 && replacedText.Substring(charCount, 2) == "\\>")
                    {
                        isInstant = true;
                        charCount++;  // Skip \> control
                        continue;
                    }

                    // Check for \<
                    if (charCount < replacedText.Length - 1 && replacedText.Substring(charCount, 2) == "\\<")
                    {
                        isInstant = false;
                        charCount++;  // Skip \< control
                        continue;
                    }

                    if (charCount < replacedText.Length - 2 && replacedText[charCount] == '\\' && replacedText[charCount + 1] == '$' 
                        && replacedText[charCount + 2] == '[')
                    {
                        // Detect the closing bracket and display everything in between
                        int closeIndex = replacedText.IndexOf("]", charCount + 2);
                        if (closeIndex > charCount)
                        {
                            var controlText = replacedText.Substring(charCount, closeIndex - charCount + 1);
                            this.Invoke(new Action(() =>
                            {
                                this.AppendText(controlText);
                            }));
                            charCount = closeIndex;
                            continue;
                        }
                    }
                    else if (charCount < replacedText.Length - 3 && replacedText[charCount] == '\\' && replacedText[charCount + 1] == '$'
                             && replacedText[charCount + 2] == 'L' && replacedText[charCount + 3] == '[')
                    {
                        // Detect the closing bracket and display everything in between
                        int closeIndex = replacedText.IndexOf("]", charCount + 2);
                        if (closeIndex > charCount)
                        {
                            var controlText = replacedText.Substring(charCount, closeIndex - charCount + 1);
                            this.Invoke(new Action(() =>
                            {
                                this.AppendText(controlText);
                            }));
                            charCount = closeIndex;
                            continue;
                        }
                    }
                    else if (charCount < replacedText.Length - 3 && replacedText[charCount] == '\\' && replacedText[charCount + 1] == '$'
                             && replacedText[charCount + 2] == 'H' && replacedText[charCount + 3] == '[')
                    {
                        // Detect the closing bracket and display everything in between
                        int closeIndex = replacedText.IndexOf("]", charCount + 2);
                        if (closeIndex > charCount)
                        {
                            var controlText = replacedText.Substring(charCount, closeIndex - charCount + 1);
                            this.Invoke(new Action(() =>
                            {
                                this.AppendText(controlText);
                            }));
                            charCount = closeIndex;
                            continue;
                        }
                    }

                    // Check for \!
                    if (charCount < replacedText.Length - 1 && replacedText.Substring(charCount, 2) == "\\!")
                    {
                        // Display a "Waiting for user input" message
                        this.Invoke(new Action(() =>
                        {
                            this.AppendText("🖱️");
                        }));
                        charCount++;  // Skip \\! control
                        continue;
                    }

                    // Check for \^
                    if (charCount < replacedText.Length - 1 && replacedText.Substring(charCount, 2) == "\\^")
                    {
                        // Display a "Automaticaly close" message
                        this.Invoke(new Action(() =>
                        {
                            this.AppendText("🔚");
                        }));
                        charCount++;  // Skip \\^ control
                        continue;
                    }

                    // Check for \w
                    if (charCount < replacedText.Length - 1 && replacedText.Substring(charCount, 2) == "\\w")
                    {
                        // Update delay
                        if (charCount < replacedText.Length - 3 && replacedText[charCount + 2] == '[')
                        {
                            int closeIndex = replacedText.IndexOf("]", charCount + 2);
                            currentDelay = float.Parse(replacedText.Substring(charCount + 3, closeIndex - charCount - 3)) * 1000;
                            charCount = closeIndex;
                        }
                        else
                        {
                            currentDelay = 500;
                            charCount++;
                        }
                        continue;
                    }

                    // Check for \c
                    if (charCount < replacedText.Length - 1 && replacedText[charCount] == '\\')
                    {
                        if (replacedText[charCount + 1] == 'c')
                        {
                            if (charCount < replacedText.Length - 7 && replacedText[charCount + 2] == '[')
                            {
                                int closeIndex = replacedText.IndexOf("]", charCount + 2);
                                if (closeIndex > charCount)
                                {
                                    var colorCode = replacedText.Substring(charCount + 3, closeIndex - charCount - 3);
                                    if (colorCode.Length == 6)
                                    {
                                        var r = int.Parse(colorCode.Substring(0, 2), NumberStyles.HexNumber);
                                        var g = int.Parse(colorCode.Substring(2, 2), NumberStyles.HexNumber);
                                        var b = int.Parse(colorCode.Substring(4, 2), NumberStyles.HexNumber);
                                        currentColor = Color.FromArgb(r, g, b);
                                    }
                                    charCount = closeIndex;
                                }
                            }
                            else
                            {
                                currentColor = Color.FromArgb(0, 0, 0); // reset to black
                                charCount++;
                            }
                            continue;
                        }
                    }

                    // Check for \z
                    if (charCount < replacedText.Length - 1 && replacedText.Substring(charCount, 2) == "\\z")
                    {
                        if (charCount < replacedText.Length - 3 && replacedText[charCount + 2] == '[')
                        {
                            int closeIndex = replacedText.IndexOf("]", charCount + 2);
                            if (closeIndex > charCount)
                            {
                                var size = float.Parse(replacedText.Substring(charCount + 3, closeIndex - charCount - 3));
                                currentSize = (int)(10.0f * size / 100.0f);
                                charCount = closeIndex;
                            }
                        }
                        else
                        {
                            charCount++; // Skip decoration character
                        }
                        continue;
                    }

                    // Check for \r
                    if (charCount < replacedText.Length - 1 && replacedText[charCount] == '\\' && replacedText[charCount + 1] == 'r')
                    {
                        if (charCount < replacedText.Length - 3 && replacedText[charCount + 2] == '[')
                        {
                            int closeIndex = replacedText.IndexOf("]", charCount + 2);
                            if (closeIndex > charCount)
                            {
                                var rubyText = replacedText.Substring(charCount + 3, closeIndex - charCount - 3);
                                var splitTexts = rubyText.Split(',');

                                if (splitTexts.Length > 1)
                                {
                                    // For \r[ルビ振り,好き勝手よみがなが打てます] style
                                    var frontText = splitTexts[0];
                                    var includedText = splitTexts[1];
                                    charCount = closeIndex;
                                    this.Invoke(new Action(() =>
                                    {
                                        this.SelectionFont = new Font(this.Font.FontFamily, currentSize);
                                        this.AppendText(frontText);
                                        this.SelectionFont = new Font(this.Font.FontFamily, currentSize / 2);
                                        this.AppendText(includedText);
                                    }));
                                }
                                else
                                {
                                    // For \r[ふ]振り style
                                    var includedText = splitTexts[0];
                                    charCount = closeIndex;
                                    this.Invoke(new Action(() =>
                                    {
                                        this.SelectionFont = new Font(this.Font.FontFamily, currentSize);
                                        this.AppendText(replacedText[charCount + 1].ToString());
                                        this.SelectionFont = new Font(this.Font.FontFamily, currentSize / 2);
                                        this.AppendText(includedText);
                                    }));
                                    charCount++;
                                }
                            }
                            continue;
                        }
                    }

                    // Check for text formatting tags
                    if (charCount < replacedText.Length - 1 && replacedText[charCount] == '\\')
                    {
                        bool recognized = true;
                        switch (replacedText[charCount + 1])
                        {
                            case 'b':
                                if (decorations.Count == 0 || decorations.Peek() != 'b') decorations.Push('b');
                                else if (decorations.Peek() == 'b') decorations.Pop();
                                isBold = !isBold;
                                break;
                            case 'i':
                                if (decorations.Count == 0 || decorations.Peek() != 'i') decorations.Push('i');
                                else if (decorations.Peek() == 'i') decorations.Pop();
                                isItalic = !isItalic;
                                break;
                            case 'u':
                                if (decorations.Count == 0 || decorations.Peek() != 'u') decorations.Push('u');
                                else if (decorations.Peek() == 'u') decorations.Pop();
                                isUnderline = !isUnderline;
                                break;
                            default:
                                recognized = false;
                                break;
                        }
                        if (recognized)
                        {
                            charCount++; // Skip decoration character
                            continue;
                        }
                    }

                    this.Invoke(new Action(() =>
                    {
                        this.SelectionFont = new Font(this.Font.FontFamily, currentSize,
                                  (isBold ? FontStyle.Bold : FontStyle.Regular)
                                | (isItalic ? FontStyle.Italic : 0)
                                | (isUnderline ? FontStyle.Underline : 0));
                        this.SelectionColor = currentColor;
                        this.AppendText(replacedText[charCount].ToString());

                        if (autoScroll == true)
                        {
                            if ((DateTime.Now - lastScrollTime).TotalMilliseconds > 500)
                            {
                                this.SelectionStart = this.Text.Length;
                                this.ScrollToCaret();
                                lastScrollTime = DateTime.Now;
                            }
                        }
                    }));

                    if (!isInstant)
                    {
                        await Task.Delay((int)currentDelay, cancelToken);
                    }
                    currentDelay = delay;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred: {ex.Message}");
                    //Console.WriteLine($"Error occurred: {ex.StackTrace}");
                }
            }
        }
        public int GetLineCount()
        {
            int lineCount = 0;
            using (Graphics graphics = this.CreateGraphics())
            {
                SizeF size = graphics.MeasureString(this.Text, this.Font, this.Width);
                lineCount = (int)Math.Ceiling(size.Height / this.Font.Height);
            }
            return lineCount;
        }
    }
}
