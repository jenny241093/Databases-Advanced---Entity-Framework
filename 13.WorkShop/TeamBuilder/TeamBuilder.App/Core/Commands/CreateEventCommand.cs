using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.VisualBasic;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;
using Constants = TeamBuilder.App.Utilities.Constants;

namespace TeamBuilder.App.Core.Commands
{
    public class CreateEventCommand
    {
        //•	CreateEvent<name> <description> <startDate> <endDate>
        public  string Execute(string[] inputArgs)
        {
            Check.CheckLength(6,inputArgs);
            var currentUser = AuthenticationManager.GetCurrentUser();

            var name = inputArgs[0];
            var description = inputArgs[1];
            DateTime startDate;
            DateTime endDate;

            bool isStartDate = DateTime.TryParseExact(inputArgs[2] + " " + inputArgs[3], Constants.DateTimeFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);

            bool isEndDate = DateTime.TryParseExact(inputArgs[4] + " " + inputArgs[5], Constants.DateTimeFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);

            if (!isEndDate||!isStartDate)
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidDateFormat);
            }
            if (startDate>endDate)
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidDateOrder);
            }
            using (var context=new TeamBuilderContext())
            {
                Event _event = new Event()
                {
                    Name = name,
                    CreatorId = currentUser.Id,
                    Description = description,
                    StartDate = startDate,
                    EndDate = endDate
                };
                context.Events.Add(_event);
                context.SaveChanges();
            }
           
           
            return $"Event {name} was created successfully!";


        }

       
        }
    }

