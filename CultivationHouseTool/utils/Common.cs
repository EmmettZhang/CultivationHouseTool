using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Forms;

namespace CultivationHouseTool
{
    public static class Common
    {
        public static AutomationElement getWindow(string title)
        {
            AutomationElement root = AutomationElement.RootElement;

            Condition condition = new PropertyCondition(AutomationElement.NameProperty, title);

            AutomationElement window = root.FindFirst(TreeScope.Children, condition);

            return window;
        }
        public static AutomationElement getButton(AutomationElement window, string title)
        {
            AutomationElement el = window.FindFirst(
                        TreeScope.Descendants,
                        new AndCondition(
                            new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button),
                            new PropertyCondition(AutomationElement.NameProperty, title)
                        )
                    );

            return el;
        }
        public static AutomationElement getButton(AutomationElement window, string title, int index)
        {
            AutomationElementCollection els = window.FindAll(
                        TreeScope.Descendants,
                        new AndCondition(
                            new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Button),
                            new PropertyCondition(AutomationElement.NameProperty, title)
                        )
                    );

            return els[index];
        }

        public static void clickButton(AutomationElement window, string title)
        {
            AutomationElement button = getButton(window, title);
            if (button != null)
            {
                InvokePattern pattern = button.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;

                pattern.Invoke();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show($"未找到按钮: {title}");
            }
        }

        public static void clickButton(AutomationElement window, string title, int index)
        {
            AutomationElement button = getButton(window, title, index);
            if (button != null)
            {
                InvokePattern pattern = button.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;

                pattern.Invoke();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show($"未找到按钮: {title}");
            }
        }

        public static void setCombo(AutomationElement window, string title)
        {
            AutomationElement combo = window.FindFirst(TreeScope.Descendants,
                                                        new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ComboBox));
            ExpandCollapsePattern expand = combo.GetCurrentPattern(ExpandCollapsePattern.Pattern) as ExpandCollapsePattern;

            expand.Expand();
            Thread.Sleep(300);

            AutomationElement item =
                combo.FindFirst(
                    TreeScope.Descendants,

                    new PropertyCondition(AutomationElement.NameProperty, title)
                );
            SelectionItemPattern select = item.GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern;

            select.Select();
        }

        public static void selectRadioButtonById(AutomationElement window, string id)
        {
            AutomationElement radio = Common.getElById(window, id);

            if (radio.TryGetCurrentPattern(
                SelectionItemPattern.Pattern,
                out object pattern))
            {
                ((SelectionItemPattern)pattern).Select();
            }
        }

        public static AutomationElement getElById(AutomationElement window, string id)
        {
            AutomationElement el =
                window.FindFirst(
                    TreeScope.Descendants,

                    new PropertyCondition(
                        AutomationElement.AutomationIdProperty,
                        id
                    )
                );

            return el;
        }

        public static void clickButtonById(AutomationElement window, string id)
        {
            AutomationElement btn = getElById(window, id);

            if (btn != null)
            {
                InvokePattern pattern = btn.GetCurrentPattern(
                        InvokePattern.Pattern
                    ) as InvokePattern;

                try
                {
                    pattern.Invoke();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show($"未找到按钮: {id}");
            }
        }

        public static void changeTab(AutomationElement window, string name, int index)
        {
            AutomationElementCollection automationElementCollection = window.FindAll(
                    TreeScope.Descendants,
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Tab)
                );

            AutomationElement item = automationElementCollection[index].FindFirst(
                TreeScope.Descendants,

                new PropertyCondition(AutomationElement.NameProperty, name)
            );

            SelectionItemPattern select = item.GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern;

            select.Select();
        }

        public static void clickLabelById(AutomationElement window, string id)
        {
            AutomationElement textControl = getElById(window, id);

            if (textControl != null)
            {
                TreeWalker walker = TreeWalker.ControlViewWalker;

                AutomationElement parent = walker.GetParent(textControl);

                IntPtr h = new IntPtr(parent.Current.NativeWindowHandle);

                var rect = textControl.Current.BoundingRectangle;

                int x = (int)(rect.Width / 2);
                int y = (int)(rect.Height / 2);

                IntPtr lParam = (IntPtr)((y << 16) | (x & 0xFFFF));

                WinApi.SendMessage(
                    h,
                    WinApi.WM_LBUTTONDOWN,
                    (IntPtr)1,
                    lParam
                );

                WinApi.SendMessage(
                    h,
                    WinApi.WM_LBUTTONUP,
                    IntPtr.Zero,
                    lParam
                );
            }
            else
            {
                System.Windows.Forms.MessageBox.Show($"未找到按钮: {id}");
            }
        }

        public static void doubleClickLabelById(AutomationElement window, string id)
        {
            AutomationElement textControl = getElById(window, id);

            if (textControl != null)
            {
                TreeWalker walker = TreeWalker.ControlViewWalker;

                AutomationElement parent = walker.GetParent(textControl);

                IntPtr h = new IntPtr(parent.Current.NativeWindowHandle);

                var rect = textControl.Current.BoundingRectangle;

                int x = (int)(rect.Width / 2);
                int y = (int)(rect.Height / 2);

                IntPtr lParam =(IntPtr)((y << 16) | (x & 0xFFFF));

                for (int i = 0; i < 2; i++)
                {
                    WinApi.SendMessage(
                        h,
                        WinApi.WM_LBUTTONDOWN,
                        (IntPtr)1,
                        lParam
                    );

                    WinApi.SendMessage(
                        h,
                        WinApi.WM_LBUTTONUP,
                        IntPtr.Zero,
                        lParam
                    );

                    Thread.Sleep(80);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show($"未找到按钮: {id}");
            }
        }

        public static void addMessage(TextBox textBox, string message)
        {
            textBox.Invoke(new Action(() =>
            {
                textBox.Text += message;
                textBox.Text += Environment.NewLine;
                textBox.SelectionStart = textBox.Text.Length;
                textBox.ScrollToCaret();
            }));
        }

        public static void clearMessage(TextBox textBox)
        {
            textBox.Invoke(new Action(() =>
            {
                textBox.Text = "";
                textBox.SelectionStart = textBox.Text.Length;
                textBox.ScrollToCaret();
            }));
        }
    }
}
