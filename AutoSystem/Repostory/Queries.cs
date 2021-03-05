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

        public void DeleteRent(int rentID)
        {
            MySqlCommand cmd = new MySqlCommand("DELETE FROM `db_auto`.`table_rent` WHERE (`RENT_ID` = @ID);", con.ConnectionDB());
            cmd.Parameters.Add("@ID", MySqlDbType.VarChar).Value = rentID;

            cmd.ExecuteNonQuery();
            con.DisconnectDB();
        }

        public void DeleteRentWithTax(int rentID)
        {
            MySqlCommand cmd = new MySqlCommand("DELETE FROM `db_auto`.`table_rent` WHERE (`RENT_ID` = @ID);", con.ConnectionDB());
            cmd.Parameters.Add("@ID", MySqlDbType.VarChar).Value = rentID;

            cmd.ExecuteNonQuery();
            con.DisconnectDB();
        }

        public void DeleteVehicle(int vehicleID)
        {
            MySqlCommand cmd = new MySqlCommand("DELETE FROM `db_auto`.`table_vehicles` WHERE (`VEHICLE_ID` = @ID);", con.ConnectionDB());
            cmd.Parameters.Add("@ID", MySqlDbType.VarChar).Value = vehicleID;

            try
            {
                cmd.ExecuteNonQuery();
                con.DisconnectDB();
            }
            catch(Exception e)
            {
                string message = e.Message;
                Console.WriteLine(message);
            }
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

        public List<Rent> RentListVehicle()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT RENT_ID,VEHICLE_MODEL,RENT_VALUE FROM TABLE_RENT JOIN TABLE_VEHICLES ON TABLE_RENT.FK_VEHICLE_ID=TABLE_VEHICLES.VEHICLE_ID;", con.ConnectionDB());
            var Rent = cmd.ExecuteReader();
            return ListAllRent(Rent);
        }

        public List<Rent> ListAllRent(MySqlDataReader dt)
        {
            var AllVehicle = new List<Rent>();
            while (dt.Read())
            {
                var RentTemp = new Rent()
                {
                    ID = int.Parse(dt["RENT_ID"].ToString()),
                    VehicleModel = dt["VEHICLE_MODEL"].ToString(),
                    ValueModel = dt["RENT_VALUE"].ToString()
                };
                AllVehicle.Add(RentTemp);
            }
            dt.Close();
            return AllVehicle;
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