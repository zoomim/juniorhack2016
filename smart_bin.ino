int fire = 2 ;
int flip = 3; 
int full = 4;
int fire_val, flip_val, full_val, prev_fire_val, prev_full_val, prev_flip_val;
int id = 1;
int fire_count = 0;
int full_count = 0;
bool to_send = 0;
int led1 = 8;
int led2 = 9;
int led3 = 10;

void setup ()
{
  //pinMode (13, OUTPUT) ;
  pinMode (fire, INPUT) ;
  pinMode(flip, INPUT);
  pinMode(full, INPUT);
  pinMode(led1, OUTPUT);
  pinMode(led2, OUTPUT);
  pinMode(led3, OUTPUT);
  Serial.begin(9600);

  //po prvním spuštění pošli jednou informace o stavu
  fire_val = digitalRead(fire);
  flip_val = digitalRead(flip);
  full_val = digitalRead(full);
  prev_fire_val = fire_val;
  prev_full_val = full_val;
  prev_flip_val = flip_val;
  
  send_data(id, full_val, flip_val, fire_val, "49:12,53.2","16:33,40.2");
}

void loop ()
{
  fire_val = digitalRead(fire);
  flip_val = digitalRead(flip);
  full_val = digitalRead(full);

  if(fire_val!=prev_fire_val)
  {
    fire_count++;
    if(fire_count>10)
    {
      fire_count=0;
      to_send=1;
      prev_fire_val=fire_val;
    }
  }
  else
  {
    fire_count=0;
  }

  if(full_val!=prev_full_val)
  {
    full_count++;
    if(full_count>50)
    {
      full_count=0;
      to_send=1;
      prev_full_val=full_val;
    }
  }
  else
  {
    full_count=0;
  }

  if(flip_val!=prev_flip_val)
  {
    to_send=1;
    prev_flip_val=flip_val;
  }


  if(to_send)
  {
    send_data(id, prev_full_val, prev_flip_val, prev_fire_val,"49:12,53.2","16:33,40.2");
    to_send=0;
  }

  delay(100);
}

void send_data(int id, bool full, bool flip, bool fire, String x, String y)
{
  Serial.print(id); //id
  Serial.print(";");
  Serial.print(full==LOW?("80"):("0")); //plnost
  Serial.print(";");
  Serial.print(flip==HIGH?("1"):("0")); //převrácenost
  Serial.print(";");
  Serial.print(fire==HIGH?("1"):("0")); //hořim?
  Serial.print(";");
  Serial.print(x); //nějaký random číslo
  Serial.print(";");
  Serial.print(y); //další random číslo
  Serial.print("\n");

  digitalWrite(full?led2:led1,LOW);
  digitalWrite(!full?led2:led1,HIGH);
}
/*void send_data(int id, int full, bool flip, bool fire, String x, String y) //overload funkce pro použití ultrazvuku
{
  Serial.print(id); //id
  Serial.print(";");
  Serial.print(full); //plnost
  Serial.print(";");
  Serial.print(flip==LOW?("1"):("0")); //převrácenost
  Serial.print(";");
  Serial.print(fire==HIGH?("1"):("0")); //hořim?
  Serial.print(";");
  Serial.print(x); //nějaký random číslo
  Serial.print(";");
  Serial.println(y); //další random číslo
}*/

