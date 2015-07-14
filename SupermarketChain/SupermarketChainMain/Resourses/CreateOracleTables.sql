-- -------------------------------------------------------------
CREATE TABLE PRODUCTS 
(
  ID NUMBER(10) NOT NULL
, Vendor_ID NUMBER(10) NOT NULL
, Product_Name VARCHAR2(50) NOT NULL
, Measure_ID NUMBER(10) NOT NULL
, Price NUMBER(19,4) NOT NULL
, CONSTRAINT PRODUCTS_PK PRIMARY KEY (ID)
, CONSTRAINT PRODUCTS_MEASURES_FK FOREIGN KEY(Measure_ID) 
  REFERENCES MEASURES(ID)
, CONSTRAINT PRODUCTS_VENDORS_FK FOREIGN KEY(Vendor_ID) 
  REFERENCES VENDORS(ID));

Create sequence prod_sequence
start with 1
increment by 1
minvalue 1
maxvalue 1000000;

-- --------------------------------------------------------------------
CREATE TABLE VENDORS 
(
  ID NUMBER(10) NOT NULL 
, VENDOR_NAME VARCHAR2(50) NOT NULL 
, CONSTRAINT VENDORS_PK PRIMARY KEY (ID));

Create sequence vend_sequence
start with 10
increment by 10
minvalue 10
maxvalue 1000000;

-- -------------------------------------------------------------------
CREATE TABLE MEASURES 
(
  ID NUMBER(10) NOT NULL 
, MEASURE_NAME VARCHAR2(50) NOT NULL 
, CONSTRAINT MEASURES_PK PRIMARY KEY (ID));

Create sequence meas_sequencea
start with 100
increment by 100
minvalue 100
maxvalue 1000000;

-- ----------------------------------------------------------------------------------
INSERT INTO PRODUCTS (ID,VENDOR_ID, PRODUCTS_NAME, MEASURE_ID,PRICE) VALUES (prod_sequence.NEXTVAL, 20, 'Beer “Zagorka”', 100, 0.86);
INSERT INTO PRODUCTS (ID,VENDOR_ID, PRODUCTS_NAME, MEASURE_ID,PRICE) VALUES (prod_sequence.NEXTVAL, 30, 'Vodka “Targovishte”', 100, 1.20);
INSERT INTO PRODUCTS (ID,VENDOR_ID, PRODUCTS_NAME, MEASURE_ID,PRICE) VALUES (prod_sequence.NEXTVAL, 10, 'Chocolate “Milka”', 200, 1.60);

-- -----------------------------------------------------------------------------
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (vend_sequence.NEXTVAL, 'Nestle Sofia Corp.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (vend_sequence.NEXTVAL, 'Zagorka Corp.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (vend_sequence.NEXTVAL, 'Targovishte Bottling Company Ltd.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (vend_sequence.NEXTVAL, 'SoftUni Drinks Ltd');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (vend_sequence.NEXTVAL, 'Aleksandar Bars Corp.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (vend_sequence.NEXTVAL, 'Svishtov Line Bottling Corp.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (vend_sequence.NEXTVAL, 'Dominos Pizza Sofiq Ltd.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (vend_sequence.NEXTVAL, 'Pirinsko Pivo Blagoevgrad Corp.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (vend_sequence.NEXTVAL, 'Kamenitza Beer Ltd');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (vend_sequence.NEXTVAL, 'Vegeta Food Corp.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (vend_sequence.NEXTVAL, 'Blue Moon Beer USA Corp.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (vend_sequence.NEXTVAL, 'CocaCola Bulgaria Ltd.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (vend_sequence.NEXTVAL, 'Pepsi Sofiq Drinks Corp.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (vend_sequence.NEXTVAL, 'Cizi Kraker Ltd.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (vend_sequence.NEXTVAL, 'Minka HomeMade Milk Corp.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (vend_sequence.NEXTVAL, 'Meet Stara Zagora Ltd.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (vend_sequence.NEXTVAL, 'Bread Bulgaria Corp.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (vend_sequence.NEXTVAL, 'Petrov Food Veliko Tarnovo Ltd.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (vend_sequence.NEXTVAL, 'Nakov Napkins Corp.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (vend_sequence.NEXTVAL, 'Angel Soda Corp.');

-- -------------------------------------------------------------------------------------------
INSERT INTO MEASURES (ID, MEASURENAME) VALUES (meas_sequence.NEXTVAL, 'liters');
INSERT INTO MEASURES (ID, MEASURENAME) VALUES (meas_sequence.NEXTVAL, 'pieces');
INSERT INTO MEASURES (ID, MEASURENAME) VALUES (meas_sequence.NEXTVAL, 'kg');
INSERT INTO MEASURES (ID, MEASURENAME) VALUES (meas_sequence.NEXTVAL, 'boxes');
INSERT INTO MEASURES (ID, MEASURENAME) VALUES (meas_sequence.NEXTVAL, 'cans');
INSERT INTO MEASURES (ID, MEASURENAME) VALUES (meas_sequence.NEXTVAL, 'bottle');
INSERT INTO MEASURES (ID, MEASURENAME) VALUES (meas_sequence.NEXTVAL, 'packets');
INSERT INTO MEASURES (ID, MEASURENAME) VALUES (meas_sequence.NEXTVAL, 'glasses');
INSERT INTO MEASURES (ID, MEASURENAME) VALUES (meas_sequence.NEXTVAL, 'grams');
