-- TABLES CREATION
CREATE TABLE VENDORS 
(
	ID NUMBER(10) NOT NULL,
	VENDOR_NAME VARCHAR2(50) NOT NULL,
	CONSTRAINT VENDORS_PK PRIMARY KEY (ID)
);

CREATE TABLE MEASURES 
(
	ID NUMBER(10) NOT NULL,
	MEASURE_NAME VARCHAR2(50) NOT NULL,
	CONSTRAINT MEASURES_PK PRIMARY KEY (ID)
);

CREATE TABLE PRODUCTS 
(
	ID NUMBER(10) NOT NULL,
	VENDOR_ID NUMBER(10) NOT NULL,
	PRODUCT_NAME VARCHAR2(50) NOT NULL,
	MEASURE_ID NUMBER(10) NOT NULL,
	PRICE NUMBER(19,4) NOT NULL,
	CONSTRAINT PRODUCTS_PK PRIMARY KEY (ID),
	CONSTRAINT PRODUCTS_MEASURES_FK FOREIGN KEY(MEASURE_ID) REFERENCES MEASURES(ID),
	CONSTRAINT PRODUCTS_VENDORS_FK FOREIGN KEY(VENDOR_ID) REFERENCES VENDORS(ID)
);

-- SEQUENCERS CREATION
CREATE SEQUENCE VENDOR_SEQUENCE
START WITH 10
INCREMENT BY 10
MINVALUE 10
MAXVALUE 1000000;

CREATE SEQUENCE MEASURE_SEQUENCE
START WITH 100
INCREMENT BY 100
MINVALUE 100
MAXVALUE 1000000;

CREATE SEQUENCE PRODUCT_SEQUENCE
START WITH 1
INCREMENT BY 1
MINVALUE 1
MAXVALUE 1000000;

-- -DATA INSERTION
-- Inserting vendors
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (VENDOR_SEQUENCE.NEXTVAL, 'Nestle Sofia Corp.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (VENDOR_SEQUENCE.NEXTVAL, 'Zagorka Corp.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (VENDOR_SEQUENCE.NEXTVAL, 'Targovishte Bottling Company Ltd.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (VENDOR_SEQUENCE.NEXTVAL, 'SoftUni Drinks Ltd');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (VENDOR_SEQUENCE.NEXTVAL, 'Aleksandar Bars Corp.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (VENDOR_SEQUENCE.NEXTVAL, 'Svishtov Line Bottling Corp.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (VENDOR_SEQUENCE.NEXTVAL, 'Dominos Pizza Sofiq Ltd.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (VENDOR_SEQUENCE.NEXTVAL, 'Pirinsko Pivo Blagoevgrad Corp.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (VENDOR_SEQUENCE.NEXTVAL, 'Kamenitza Beer Ltd');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (VENDOR_SEQUENCE.NEXTVAL, 'Vegeta Food Corp.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (VENDOR_SEQUENCE.NEXTVAL, 'Blue Moon Beer USA Corp.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (VENDOR_SEQUENCE.NEXTVAL, 'CocaCola Bulgaria Ltd.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (VENDOR_SEQUENCE.NEXTVAL, 'Pepsi Sofiq Drinks Corp.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (VENDOR_SEQUENCE.NEXTVAL, 'Cizi Kraker Ltd.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (VENDOR_SEQUENCE.NEXTVAL, 'Minka HomeMade Milk Corp.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (VENDOR_SEQUENCE.NEXTVAL, 'Meet Stara Zagora Ltd.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (VENDOR_SEQUENCE.NEXTVAL, 'Bread Bulgaria Corp.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (VENDOR_SEQUENCE.NEXTVAL, 'Petrov Food Veliko Tarnovo Ltd.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (VENDOR_SEQUENCE.NEXTVAL, 'Nakov Napkins Corp.');
INSERT INTO VENDORS (ID, VENDOR_NAME) VALUES (VENDOR_SEQUENCE.NEXTVAL, 'Angel Soda Corp.');

-- Inserting measures
INSERT INTO MEASURES (ID, MEASURE_NAME) VALUES (MEASURE_SEQUENCE.NEXTVAL, 'liters');
INSERT INTO MEASURES (ID, MEASURE_NAME) VALUES (MEASURE_SEQUENCE.NEXTVAL, 'pieces');
INSERT INTO MEASURES (ID, MEASURE_NAME) VALUES (MEASURE_SEQUENCE.NEXTVAL, 'kg');
INSERT INTO MEASURES (ID, MEASURE_NAME) VALUES (MEASURE_SEQUENCE.NEXTVAL, 'boxes');
INSERT INTO MEASURES (ID, MEASURE_NAME) VALUES (MEASURE_SEQUENCE.NEXTVAL, 'cans');
INSERT INTO MEASURES (ID, MEASURE_NAME) VALUES (MEASURE_SEQUENCE.NEXTVAL, 'bottle');
INSERT INTO MEASURES (ID, MEASURE_NAME) VALUES (MEASURE_SEQUENCE.NEXTVAL, 'packets');
INSERT INTO MEASURES (ID, MEASURE_NAME) VALUES (MEASURE_SEQUENCE.NEXTVAL, 'glasses');
INSERT INTO MEASURES (ID, MEASURE_NAME) VALUES (MEASURE_SEQUENCE.NEXTVAL, 'grams');

-- Inserting products
INSERT INTO PRODUCTS (ID,VENDOR_ID, PRODUCT_NAME, MEASURE_ID,PRICE) VALUES (PRODUCT_SEQUENCE.NEXTVAL, 20, 'Beer �Zagorka�', 100, 0.86);
INSERT INTO PRODUCTS (ID,VENDOR_ID, PRODUCT_NAME, MEASURE_ID,PRICE) VALUES (PRODUCT_SEQUENCE.NEXTVAL, 30, 'Vodka �Targovishte�', 100, 1.20);
INSERT INTO PRODUCTS (ID,VENDOR_ID, PRODUCT_NAME, MEASURE_ID,PRICE) VALUES (PRODUCT_SEQUENCE.NEXTVAL, 10, 'Chocolate �Milka�', 200, 1.60);
l di

SELECT * FROM VENDORS;
SELECT * FROM MEASURES;
