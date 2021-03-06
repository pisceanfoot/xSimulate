﻿using System;
using System.Threading;
using System.Windows.Forms;
using xSimulate.Action;
using xSimulate.Browse;

namespace xSimulate.WebAutomationTasks
{
    public class ScrollTask : CommonTask
    {
        private System.Threading.Timer timer;
        private Random random;
        private int bottom = 0;
        private bool running = true;
        private int lastY = -1;
        private bool positive = true;
        private int period = 0;

        public ScrollTask(AutomationManagement manager)
            : base(manager)
        {
            timer = new System.Threading.Timer(new TimerCallback(timer_Tick));
            timer.Change(Timeout.Infinite, Timeout.Infinite);

            random = new Random();
        }

        #region Task

        private void InitScroll()
        {
            this.lastY = -1;
            this.running = true;
        }

        protected override void OnProcess(Action.IAction action)
        {
            ScrollAction scrollAction = action as ScrollAction;
            if (scrollAction == null)
            {
                return;
            }

            LoggerManager.Debug(action.AutomationActionData);

            // page
            if (scrollAction.Position == Position.None)
            {
                return;
            }

            InitScroll();

            this.period = scrollAction.Period;

            if (scrollAction.Position == Position.PageBottom)
            {
                bottom = GetMaxPosition();
            }
            else if (scrollAction.Position == Position.Middle)
            {
                bottom = GetMaxPosition();
                bottom = bottom / 2;
            }
            else if (scrollAction.Position == Position.Element)
            {
                HtmlElement element = this.GetData(action) as HtmlElement;
                if (element == null)
                {
                    LoggerManager.Error("Element Not Found");
                    throw new ElementNoFoundException("Element Not Found", action);
                }
                bottom = GetY(element);

                if (bottom == 0)
                {
                    bottom = GetYoffset(element);
                }
            }

            if (scrollAction.Factor > 0)
            {
                bottom = bottom / scrollAction.Factor;
            }

            int current = GetY();
            if (bottom == current)
            {
                return;
            }
            else if (bottom > current)
            {
                positive = true;
                bottom += scrollAction.Offset;
            }
            else
            {
                positive = false;
                bottom -= scrollAction.Offset;
            }

            timer.Change(this.period, Timeout.Infinite);
        }

        public override bool IsComplete()
        {
            return !running;
        }

        #endregion Task

        #region Scroll

        private void timer_Tick(object obj)
        {
            timer.Change(Timeout.Infinite, Timeout.Infinite);
            if (Process())
            {
                running = false;
                return;
            }

            timer.Change(this.period, Timeout.Infinite);
        }

        private bool Process()
        {
            int y = GetY();

            if (lastY == y)
            {
                return true;
            }

            if (positive)
            {
                if (y >= bottom)
                {
                    return true;
                }
            }
            else
            {
                if (y <= bottom)
                {
                    return true;
                }
            }

            lastY = y;
            if (bottom != y)
            {
                int step = random.Next(positive ? 100 : -200, positive ? 200 : -100);
                ToY(y + step);

                return false;
            }

            return true;
        }

        #endregion Scroll

        #region Get Position

        private int GetMaxPosition()
        {
            return this.Call<WebBrowserEx, int>(delegate(WebBrowserEx ex)
            {
                return ex.Document.Window.Size.Height;
            }, this.webBrowser);
        }

        private int GetY()
        {
            return this.Call<WebBrowserEx, int>(delegate(WebBrowserEx ex)
            {
                return ex.Document.GetElementsByTagName("HTML")[0].ScrollTop;
            }, this.webBrowser);
        }

        private int GetY(HtmlElement element)
        {
            return this.Call<HtmlElement, int>(delegate(HtmlElement ex)
            {
                return ex.ScrollTop;
            }, element);
        }

        private void ToY(int y)
        {
            this.Call<WebBrowserEx>(delegate(WebBrowserEx ex)
            {
                ex.Document.Window.ScrollTo(0, y);
            }, this.webBrowser);
        }

        public int GetXoffset(HtmlElement element)
        {
            return this.Call<HtmlElement, int>(delegate(HtmlElement el)
            {
                //get element pos
                int xPos = el.OffsetRectangle.Left;

                //get the parents pos
                HtmlElement tempEl = el.OffsetParent;
                while (tempEl != null)
                {
                    xPos += tempEl.OffsetRectangle.Left;
                    tempEl = tempEl.OffsetParent;
                }

                return xPos;
            }, element);
        }

        public int GetYoffset(HtmlElement element)
        {
            return this.Call<HtmlElement, int>(delegate(HtmlElement el)
            {
                //get element pos
                int yPos = el.OffsetRectangle.Top;

                //get the parents pos
                HtmlElement tempEl = el.OffsetParent;
                while (tempEl != null)
                {
                    yPos += tempEl.OffsetRectangle.Top;
                    tempEl = tempEl.OffsetParent;
                }

                return yPos;
            }, element);
        }

        #endregion Get Position
    }
}