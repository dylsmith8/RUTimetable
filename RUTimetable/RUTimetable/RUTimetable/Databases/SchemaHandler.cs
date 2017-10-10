using System;
using System.Linq;
using Realms;

namespace RUTimetable
{
	public class SchemaHandler
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:RUTimetable.SchemaHandler"/> class.
		/// Responsible for handling any schema changes made to the current data models contained in the database
		/// To ensure no data is lost when new properties are added to the datamodels
		/// </summary>
		public SchemaHandler()
		{
			//ToDo 
		}
		public RealmConfiguration HandleChanges()
		{
			//Todo complete the Migration
		var config = new RealmConfiguration
		{
			SchemaVersion = 2,
			MigrationCallback = (migration, oldSchemaVersion) =>
			{
				var studentNew = migration.NewRealm.All<Student>();
				var studentOld = migration.OldRealm.All<Student>();
			}
		};

			return config;
		}
	}
}
