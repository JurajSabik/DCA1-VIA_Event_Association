﻿using System.Text.Json;

namespace ViaEventAssociation.Infrastructure.EfcQueries.SeedFactories;

public record TmpLocation(string Id, string Name, int MaxCapacity, string AvailabilityStart, string AvailabilityEnd);

public class LocationSeedFactory
{

  public static List<DTOs.Location> CreateLocations(string creatorId)
  {
    var tmpLocations = JsonSerializer.Deserialize<List<TmpLocation>>(LocationJson);
    
    var locations = tmpLocations!.Select(l=> new DTOs.Location
    {
      CreatorId = creatorId,
      
      Id = l.Id,
      LocationName = l.Name,
      MaxGuests = l.MaxCapacity,
      Start = l.AvailabilityStart,
      End = l.AvailabilityEnd
    }).ToList();

    return locations;
  }
  
  private const string LocationJson = """
                                        [
                                          {
                                            "Id": "e0b61b58-0af5-4d64-b801-27153bdf1c01",
                                            "Name": "Atriet",
                                            "MaxCapacity": 50,
                                            "AvailabilityStart": "11-04-2024",
                                            "AvailabilityEnd": "14-04-2024"
                                          },
                                          {
                                            "Id": "efd18631-e050-4bd0-a15a-6d5a17cca490",
                                            "Name": "C05.19",
                                            "MaxCapacity": 40,
                                            "AvailabilityStart": "11-04-2024",
                                            "AvailabilityEnd": "14-04-2024"
                                          },
                                          {
                                            "Id": "6a739f77-5a4d-4e03-9631-9fdfd1403975",
                                            "Name": "C05.15",
                                            "MaxCapacity": 40,
                                            "AvailabilityStart": "01-02-2024",
                                            "AvailabilityEnd": "30-04-2024"
                                          },
                                          {
                                            "Id": "def55e29-c806-4cc1-a0f5-91757446212a",
                                            "Name": "Aud A1",
                                            "MaxCapacity": 50,
                                            "AvailabilityStart": "01-03-2024",
                                            "AvailabilityEnd": "30-06-2024"
                                          },
                                          {
                                            "Id": "731cea3a-4e31-4af5-b537-7b35c3dbe73c",
                                            "Name": "Guest Canteen",
                                            "MaxCapacity": 50,
                                            "AvailabilityStart": "01-03-2024",
                                            "AvailabilityEnd": "03-05-2024"
                                          },
                                          {
                                            "Id": "e4c821b2-2b98-49f6-b533-49ac8efd5d00",
                                            "Name": "C02.11",
                                            "MaxCapacity": 40,
                                            "AvailabilityStart": "10-02-2024",
                                            "AvailabilityEnd": "10-05-2024"
                                          },
                                          {
                                            "Id": "cc395a25-5b53-41de-a5bf-258c90a5bc43",
                                            "Name": "A04.13",
                                            "MaxCapacity": 20,
                                            "AvailabilityStart": "01-03-2024",
                                            "AvailabilityEnd": "30-04-2024"
                                          },
                                          {
                                            "Id": "da6c8bde-d7d6-4921-9512-ae469d6a3f4e",
                                            "Name": "Grassfield",
                                            "MaxCapacity": 50,
                                            "AvailabilityStart": "15-01-2024",
                                            "AvailabilityEnd": "15-05-2024"
                                          },
                                          {
                                            "Id": "eb34cf58-a348-4400-8ace-2ad775912db7",
                                            "Name": "Soccerfield",
                                            "MaxCapacity": 50,
                                            "AvailabilityStart": "01-02-2024",
                                            "AvailabilityEnd": "30-05-2024"
                                          }
                                        ]
                                        """;
}