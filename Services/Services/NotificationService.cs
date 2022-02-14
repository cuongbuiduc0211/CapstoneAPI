using CorePush.Google;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Utility.Models;
using static Utility.Models.GoogleNotification;
namespace Services.Services
{
    public class NotificationService
    {
        private readonly FcmNotificationSetting _fcmNotificationSetting;
        public NotificationService(IOptions<FcmNotificationSetting> settings)
        {
            _fcmNotificationSetting = settings.Value;
        }

        public async Task<bool> SendNoti(string clientToken, string title, string body)
        {
            var message = new Message()
            {
                Token = clientToken,
                Notification = new Notification()
                {
                    Title = title,
                    Body = body,
                    
                },
                
            };
            string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            
            if (response != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> SendNotiToMobile(string clientToken, string title, string body, string image, string redirect)
        {
            var message = new Message()
            {
                Token = clientToken,
                Notification = new Notification()
                {
                    Title = title,
                    Body = body,

                },
                Data = new Dictionary<string, string>
                {
                    {"click_action",  "FLUTTER_NOTIFICATION_CLICK" },
                    {"sound",  "default" },
                    {"image",  image },
                    {"redirect", redirect}
                }
            };
            string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            
            if (response != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public async Task<ResponseModel> SendNotification(NotificationModel notiModel)
        //{

        //    ResponseModel response = new ResponseModel();
        //    try
        //    {
        //        FcmSettings settings = new FcmSettings()
        //        {
        //            SenderId = _fcmNotificationSetting.SenderId,
        //            ServerKey = _fcmNotificationSetting.ServerKey
        //        };
        //        HttpClient httpClient = new HttpClient();

        //        string authorizationKey = string.Format("keyy={0}", settings.ServerKey);
        //        string deviceToken = notiModel.DeviceId;

        //        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
        //        httpClient.DefaultRequestHeaders.Accept
        //                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        DataPayload dataPayload = new DataPayload();
        //        dataPayload.Title = notiModel.Title;
        //        dataPayload.Body = notiModel.Body;

        //        GoogleNotification notification = new GoogleNotification();
        //        notification.Data = dataPayload;
        //        notification.Notification = dataPayload;

        //        var fcm = new FcmSender(settings, httpClient);
        //        var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);

        //        if (fcmSendResponse.IsSuccess())
        //        {
        //            response.IsSuccess = true;
        //            response.Message = "Notification sent successfully";
        //            return response;
        //            //return fcmSendResponse;
        //        }
        //        else
        //        {
        //            response.IsSuccess = false;
        //            response.Message = fcmSendResponse.Results[0].Error;
        //            return response;
        //            //return null;
        //        }
        //    }
        //    catch
        //    {
        //        response.IsSuccess = false;
        //        response.Message = "Something went wrong";
        //        return response;
        //        //return null;
        //    }
        //}
    }
}
