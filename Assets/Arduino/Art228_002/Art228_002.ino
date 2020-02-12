int ledPin = 13; // LED connected to digital pin 13
int inPin = 7;   // pushbutton connected to digital pin 7
int val = 0;     // variable to store the read value
boolean hasUserIn;

void setup()
{
  Serial.begin(9600);
  pinMode(ledPin, OUTPUT);      // sets the digital pin 13 as output
  pinMode(inPin, INPUT);      // sets the digital pin 7 as input
  hasUserIn = false;
}

void loop()
{
  val = digitalRead(inPin);   // read the input pin
  
  if(val==0) {  //if no user
    //Serial.write(0);
    //Serial.println(0, DEC);
    digitalWrite(ledPin, LOW);
    hasUserIn = false;
  }
  if(val==1) {  // if user go in
    if(hasUserIn == false) {
      //Serial.write(1);
      //Serial.println(1, DEC);
      Serial.println("1");
      digitalWrite(ledPin, HIGH);
      hasUserIn = true;
    }
  }
  delay(50);
}
