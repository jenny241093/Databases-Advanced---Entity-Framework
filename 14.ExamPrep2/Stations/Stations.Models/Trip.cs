﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Stations.Models.Enums;

namespace Stations.Models
{
    public class Trip
    {
        public int Id { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        public int OriginStationId { get; set; }

        [Required]
        public Station OriginStation { get; set; }

        public int DestinationStationId { get; set; }

        [Required]
        public Station DestinationStation { get; set; }

        public int TrainId { get; set; }

        [Required]
        public Train Train { get; set; }

        [Required]
        public TripStatus Status { get; set; }

        public TimeSpan? TimeDifference { get; set; }

        public ICollection<Ticket> Tickets { get; set; }=new List<Ticket>();
    }
}