int smoke = 2 ;
int flip = 3; 
int full = 4;
int val, val2, val3;
void setup ()
{
  pinMode (13, OUTPUT) ;
  //digitalWrite(13,HIGH);
  pinMode (smoke, INPUT) ;
  pinMode(flip, INPUT);
  pinMode(full, INPUT); 
  Serial.begin(9600);
}
void loop ()
{
  val = digitalRead(smoke) ;
  val2 = digitalRead(flip);
  val3 = digitalRead(full);
  Serial.print("1"); //id
  Serial.print(";");
  Serial.print(val3==LOW?("1"):("0")); //plnost
  //Serial.print(val);
  Serial.print(";");
  Serial.print(val2==LOW?("0"):("1")); //převrácenost
  Serial.print(";");
  Serial.print(val==HIGH?("1"):("0")); //hořim?
  Serial.print(";");
  Serial.print("49.213576"); //nějaký random číslo
  Serial.print(";");
  Serial.println("16.559680"); //další random číslo
  /*if (val2==HIGH) // When the obstacle avoidance sensor detects a signal, LED flashes
  {
    digitalWrite (13, HIGH);
  }
  else
  {
    digitalWrite (13, LOW);
  }*/
  delay(10000);
}
