int ledPin = 13; // LED connected to digital pin 13
int inPin = 7;   // pushbutton connected to digital pin 7
int val = 0;     // variable to store the read value
int tamp = 10;

void setup()
{
  Serial.begin(9600);
  pinMode(ledPin, OUTPUT);      // sets the digital pin 13 as output
  pinMode(inPin, INPUT);      // sets the digital pin 7 as input
}

void loop()
{
  val = digitalRead(inPin);   // read the input pin
  
  if(val==0)
  {
    if(tamp!=0)
    {
    Serial.write(0);
    //Serial.println(0, DEC);
    digitalWrite(ledPin, LOW);
    tamp=0;
    }  
  }
 
  if(val==1)
  {
    if(tamp!=1)
    {
    Serial.write(1);
    //Serial.println(1, DEC);
    digitalWrite(ledPin, HIGH);
    tamp=1;
    }  
  }

  delay(50);


  
}
