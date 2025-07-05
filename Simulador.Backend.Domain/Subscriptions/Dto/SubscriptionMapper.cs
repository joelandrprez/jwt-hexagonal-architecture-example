using Simulador.Backend.Domain.Auth.Domain;
using Simulador.Backend.Domain.Auth.Dto;
using Simulador.Backend.Domain.Courses.Domain;
using Simulador.Backend.Domain.Courses.Dto;
using Simulador.Backend.Domain.Subscriptions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Simulador.Backend.Domain.Subscriptions.Dto
{
    public class SubscriptionMapper
    {
        public static SubscriptionResponse ToDto(Subscription subscription)
        {
            if (subscription == null || subscription.StatusSubscription == null) return null;


            StatusSubscriptionResponse statusSubscriptionResponse = new StatusSubscriptionResponse();
            statusSubscriptionResponse.Id = subscription.Id;
            statusSubscriptionResponse.Name = subscription.Name;
            statusSubscriptionResponse.Description = subscription.Description;

            return new SubscriptionResponse
            {
                Id  = subscription.Id  ,
                Name = subscription.Name ,
                Description = subscription.Description ,
                ExpirationDate = subscription.ExpirationDate ,
                SubscriptionDate = subscription.SubscriptionDate ,
                CourseId = subscription.CourseId ,
                StatusId = subscription.StatusId ,
                UserId = subscription.UserId,
                Course = CourseToDto(subscription.Course),
                Status = StatusSubscriptionToDto(subscription.StatusSubscription)
            };
        }

        public static StatusSubscriptionResponse StatusSubscriptionToDto(StatusSubscription statusSubscription)
        {
            if (statusSubscription == null) return null;

            return new StatusSubscriptionResponse
            {
                Id = statusSubscription.Id,
                Name = statusSubscription.Name,
                Description = statusSubscription.Description
            };
        }

        public static CourseResponse CourseToDto(Course? course)
        {
            if (course == null) return null;

            return new CourseResponse
            {
                Id = course.Id,
                Name = course.Name,
                EntityId = course.EntityId,
                QuestionCount = course.QuestionCount,
                TimeMinute = course.TimeMinute
            };
        }

        public static Subscription ToDomain(SubscriptionRequest subscriptionRequest)
        {
            return new Subscription
            {
                Id = subscriptionRequest.Id ,
                Name = subscriptionRequest.Name ,
                Description = subscriptionRequest.Description ,
                ExpirationDate = subscriptionRequest.ExpirationDate ,
                SubscriptionDate = subscriptionRequest.SubscriptionDate ,
                CourseId = subscriptionRequest.CourseId ,
                StatusId = subscriptionRequest.StatusId ,
                UserId = subscriptionRequest.UserId,
                CreatedAt = null,
                CreatedBy = null,
                UpdatedAt = null,
                UpdatedBy = null,
                DeletedAt = null,
                DeletedBy = null,
            };
        }
    }
}
