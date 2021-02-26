using AutoSystem.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoSystem.Repostory
{
    public class Queries
    {
        Connection con = new Connection();

        public void RegisterVehicle(Vehicle vehicle)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO `db_auto`.`table_vehicles` (`VEHICLE_MODEL`, `RENT_VALUE`) VALUES (@Name, @Value);", con.ConnectionDB());
            cmd.Parameters.Add("@Name", MySqlDbType.VarChar).Value = vehicle.Name;
            cmd.Parameters.Add("@Value", MySqlDbType.VarChar).Value = vehicle.Value;

            cmd.ExecuteNonQuery();
            con.DisconnectDB();

        }

        public void RegisterRent(Rent rent)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO `db_auto`.`table_rent` (`FK_VEHICLE_ID`) VALUES (@ID);", con.ConnectionDB());
            cmd.Parameters.Add("@ID", MySqlDbType.VarChar).Value = rent.Vehicle.ID;

            cmd.ExecuteNonQuery();
            con.DisconnectDB();

        }

        public void DeleteVehicle(int vehicleID)
        {
            MySqlCommand cmd = new MySqlCommand("DELETE FROM `db_auto`.`table_vehicles` WHERE (`VEHICLE_ID` = @ID);", con.ConnectionDB());
            cmd.Parameters.Add("@ID", MySqlDbType.VarChar).Value = vehicleID;

            cmd.ExecuteNonQuery();
            con.DisconnectDB();
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            MySqlCommand cmd = new MySqlCommand("UPDATE `db_auto`.`table_vehicles` SET `VEHICLE_MODEL` = @Name, `RENT_VALUE` = @Value WHERE (`VEHICLE_ID` = @ID);", con.ConnectionDB());
            cmd.Parameters.Add("@ID", MySqlDbType.VarChar).Value = vehicle.ID;
            cmd.Parameters.Add("@Name", MySqlDbType.VarChar).Value = vehicle.Name;
            cmd.Parameters.Add("@Value", MySqlDbType.VarChar).Value = vehicle.Value;

            cmd.ExecuteNonQuery();
            con.DisconnectDB();
        }

        public Vehicle GetVehicleById(int vehicleID)
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM db_auto.table_vehicles where VEHICLE_ID = @ID;", con.ConnectionDB());
            cmd.Parameters.Add("@ID", MySqlDbType.VarChar).Value = vehicleID;

            MySqlDataReader reader;


            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Vehicle dto = new Vehicle();
                    {
                        dto.ID = Convert.ToInt32(reader[0]);
                        dto.Name = Convert.ToString(reader[1]);
                        dto.Value = Convert.ToDouble(reader[2]);

                        return dto;
                    }
                }
            }
            else
            {
                //return null;
            }
            reader.Close();
            Vehicle a = new Vehicle();
            return a;
        }

        public List<Vehicle> ListVehicle()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM db_auto.table_vehicles;", con.ConnectionDB());
            var VehicleDatas = cmd.ExecuteReader();
            return ListAllVehicle(VehicleDatas);
        }

        public List<Vehicle> ListAllVehicle(MySqlDataReader dt)
        {
            var AllVehicle = new List<Vehicle>();
            while (dt.Read())
            {
                var VehicleTemp = new Vehicle()
                {
                    ID = int.Parse(dt["VEHICLE_ID"].ToString()),
                    Name = dt["VEHICLE_MODEL"].ToString(),
                    Value = int.Parse(dt["RENT_VALUE"].ToString()),
                };
                AllVehicle.Add(VehicleTemp);
            }
            dt.Close();
            return AllVehicle;
        }
    }
}