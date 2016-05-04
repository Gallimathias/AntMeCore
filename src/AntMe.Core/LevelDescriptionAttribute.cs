﻿using System;
using System.Resources;

namespace AntMe
{
    /// <summary>
    /// Level Description Attribute to hold all relevant Information about a Level.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    [Serializable]
    public sealed class LevelDescriptionAttribute : Attribute
    {
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="guid">Guid of this Level</param>
        /// <param name="mapType">Type of the Map</param>
        /// <param name="name">Name of this Level</param>
        /// <param name="description">Short Level Description</param>
        public LevelDescriptionAttribute(string guid, Type mapType, string name, string description)
        {
            Init(guid, mapType, name, description);
        }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="guid">Guid of this Level</param>
        /// <param name="mapType">Type of the Map</param>
        /// <param name="resourceType">Type of Resource Class for Name and Description</param>
        /// <param name="nameKey">Resource Key for the Level Name</param>
        /// <param name="descriptionKey">Resource Key for the Level Description</param>
        public LevelDescriptionAttribute(string guid, Type mapType, Type resourceType, string nameKey, string descriptionKey)
        {
            // Ressourcen auflösen und Strings auslesen
            var resourceManager = new ResourceManager(resourceType);
            string name = resourceManager.GetString(nameKey);
            string description = resourceManager.GetString(descriptionKey);

            Init(guid, mapType, name, description);
        }

        /// <summary>
        /// Initializes the Attribute Stuff and checks the data.
        /// </summary>
        /// <param name="guid">Guid of this Level</param>
        /// <param name="mapType">Type of the Map</param>
        /// <param name="name">Name of this Level</param>
        /// <param name="description">Short Level Description</param>
        private void Init(string guid, Type mapType, string name, string description)
        {
            // Check for valid ID
            Guid id;
            if (!Guid.TryParse(guid, out id))
                throw new ArgumentException("Invalid Guid Format in Level Description.");

            // Check Name
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name", string.Format("The Level Desciption with the ID {0} has no valid Name", id.ToString()));

            // Check Description
            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException("description", string.Format("The Level Desciption with the ID {0} and Name '{1}' has no valid Description", id.ToString(), name));

            // Check Map
            if (mapType == null)
                throw new ArgumentNullException("mapType", string.Format("The Level Desciption with the Name '{0}' has no valid Map", name));
            if (mapType.IsAbstract)
                throw new ArgumentException(string.Format("The Level Desciption with the Name '{0}' uses an abstract Map", name));
            if (mapType.GetConstructor(new Type[] { }) == null)
                throw new ArgumentException(string.Format("The Level Desciption with the Name '{0}' uses a Map without a parameterless constructor", name));
            Map map = (Map)Activator.CreateInstance(mapType);
            map.CheckMap();

            Id = id;
            Name = name;
            Description = description;
            Map = map;

            MinPlayerCount = 0;
            MaxPlayerCount = 8;
            Hidden = false;
        }

        /// <summary>
        /// Level ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the Level.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Short Description of the Level.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Maximum Count of Players for the Level.
        /// </summary>
        public int MaxPlayerCount { get; set; }

        /// <summary>
        /// Minimum Count of Players for the Level.
        /// </summary>
        public int MinPlayerCount { get; set; }

        /// <summary>
        /// Is this Level free for play or hidden in an Campaign?
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        /// Returns the Map for this Level.
        /// </summary>
        public Map Map { get; set; }

        /// <summary>
        /// Validates all Level Description Properties.
        /// </summary>
        public void Validate()
        {
            // ID prüfen
            if (Id == Guid.Empty)
                throw new ArgumentException("Id kann nicht empty sein");

            // Name prüfen
            if (string.IsNullOrEmpty(Name))
                throw new ArgumentException("Name kann nicht leer sein");

            // Description prüfen
            if (string.IsNullOrEmpty(Description))
                throw new ArgumentException("Description kann nicht leer sein");

            // Map prüfen
            if (Map == null)
                throw new ArgumentException("Map darf nicht null sein");
            Map.CheckMap();

            // Min Player
            if (MinPlayerCount < 0 || MinPlayerCount > 8)
                throw new ArgumentOutOfRangeException("MinPlayerCount muss zwischen 0 und 8 liegen.");

            // Max Player
            if (MaxPlayerCount < 0 || MaxPlayerCount > 8)
                throw new ArgumentOutOfRangeException("MaxPlayerCount muss zwischen 0 und 8 liegen.");
        }
    }
}