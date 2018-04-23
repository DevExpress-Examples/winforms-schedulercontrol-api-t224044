﻿using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using System;
using System.Drawing;
using System.Linq;

namespace SchedulerAPISample.CodeExamples {
    class CustomizingVisualElements {

        static void InitAppointmentDisplayTextEvent(SchedulerControl scheduler) {
            #region #InitAppointmentDisplayTextEvent
            scheduler.InitAppointmentDisplayText += scheduler_InitAppointmentDisplayText;
            scheduler.ActiveView.LayoutChanged();
            #endregion #InitAppointmentDisplayTextEvent
        }

        #region #@InitAppointmentDisplayTextEvent
        public static void scheduler_InitAppointmentDisplayText(object sender, AppointmentDisplayTextEventArgs e) {
            // Display custom text in Day and WorkWeek views only (VerticalAppointmentViewInfo).
            if (e.ViewInfo is VerticalAppointmentViewInfo) {
                e.Text = e.Appointment.Subject + "\r\n";
                e.Text += "------\r\n";
                if (e.Description != String.Empty) {
                    e.Description = string.Empty;
                    e.Text += "Description is hidden";
                }
            }
        }
        #endregion #@InitAppointmentDisplayTextEvent

        static void InitAppointmentImagesEvent(SchedulerControl scheduler) {
            #region #InitAppointmentImagesEvent
            scheduler.InitAppointmentImages += scheduler_InitAppointmentImages;
            scheduler.ActiveView.LayoutChanged();
            #endregion #InitAppointmentImagesEvent
        }

        #region #@InitAppointmentImagesEvent
        public static void scheduler_InitAppointmentImages(object sender, AppointmentImagesEventArgs e) {
            AppointmentImageInfo info = new AppointmentImageInfo();
            info.Image = Image.FromFile("image.png");
            e.ImageInfoList.Add(info);
        }
        #endregion #@InitAppointmentImagesEvent


        static void LayoutViewInfoCustomizingEvent(SchedulerControl scheduler) {
            #region #LayoutViewInfoCustomizingEvent
            scheduler.LayoutViewInfoCustomizing += scheduler_LayoutViewInfoCustomizing; ;
            scheduler.ActiveView.LayoutChanged();
            #endregion #LayoutViewInfoCustomizingEvent
        }

        #region #@LayoutViewInfoCustomizingEvent
        public static void scheduler_LayoutViewInfoCustomizing(object sender, LayoutViewInfoCustomizingEventArgs e) {
            string s = e.ViewInfo.GetType().ToString().Substring("DevExpress.XtraScheduler.Drawing.".Length);
            if (e.Kind == LayoutElementKind.DateHeader) {
                SchedulerHeader header = e.ViewInfo as SchedulerHeader;
                if (header != null) header.Caption = s;
            }
            if (e.Kind == LayoutElementKind.Cell) {
                SchedulerViewCellBase cell = e.ViewInfo as SchedulerViewCellBase;
                if (cell != null) cell.Appearance.BackColor = Color.LightYellow;
                SingleWeekCellBase cellWeek = e.ViewInfo as SingleWeekCellBase;
                if (cellWeek != null) {
                    cellWeek.Appearance.BackColor = Color.LightCyan;
                    cellWeek.Header.Caption = s;
                }
            }
        }
        #endregion #@LayoutViewInfoCustomizingEvent

        static void CustomizeDateNavigationBarCaptionEvent(SchedulerControl scheduler) {
            #region #CustomizeDateNavigationBarCaptionEvent
            scheduler.CustomizeDateNavigationBarCaption += scheduler_CustomizeDateNavigationBarCaption;
            scheduler.ActiveView.LayoutChanged();
            #endregion #CustomizeDateNavigationBarCaptionEvent
        }

        #region #@CustomizeDateNavigationBarCaptionEvent
        public static void scheduler_CustomizeDateNavigationBarCaption(object sender, CustomizeDateNavigationBarCaptionEventArgs e) {
            e.Caption = String.Format("Displaying dates from {0:D} to {1:D}", e.Interval.Start.Date, e.Interval.End.Date);
        }
        #endregion #@CustomizeDateNavigationBarCaptionEvent

        static void CustomizeMesssageBoxCaptionEvent(SchedulerControl scheduler) {
            #region #CustomizeMesssageBoxCaptionEvent
            scheduler.CustomizeMessageBoxCaption += scheduler_CustomizeMessageBoxCaption;
            scheduler.ActiveView.LayoutChanged();
            #endregion #CustomizeMesssageBoxCaptionEvent
        }

        #region #@CustomizeMesssageBoxCaptionEvent
        public static void scheduler_CustomizeMessageBoxCaption(object sender, CustomizeMessageBoxCaptionEventArgs e) {
            if (e.CaptionId == DevExpress.XtraScheduler.Localization.SchedulerStringId.Msg_SaveBeforeClose)
                e.Caption = "Appointment modification";
        }
        #endregion #@CustomizeMesssageBoxCaptionEvent

    }
}
