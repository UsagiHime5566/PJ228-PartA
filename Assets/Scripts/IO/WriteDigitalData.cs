using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class WriteDigitalData : MonoBehaviour {
	
	private SerialPort arduinoPort;
	public bool ArduinoPortState { get {
		if(arduinoPort == null)
			return false;
			
		return arduinoPort.IsOpen;
		} }

	public bool DvInit (string comName)
	{
		arduinoPort = new SerialPort( comName, 9600 );
		
		if( arduinoPort.IsOpen == false )
		{
			arduinoPort.Open();
			arduinoPort.WriteTimeout = 100;
			Debug.Log( "Open port sucessful!!" );
		}
		else
		{
			Debug.Log( "Port already opened!!" );
			return false;
		}
		
		return true;
	}

	public void WriteData(string data){
		if(arduinoPort == null)
			return;

		if(!arduinoPort.IsOpen)
			return;

		arduinoPort.WriteLine(data);
	}

	public void CloseArduino(){
		if(arduinoPort == null)
			return;

		arduinoPort.Close();
	}
}
