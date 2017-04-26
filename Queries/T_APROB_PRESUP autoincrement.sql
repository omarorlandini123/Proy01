CREATE SEQUENCE T_APROB_PRESUP_AI START WITH 1;
CREATE OR REPLACE TRIGGER T_APROB_PRESUP_AI_TRIG
BEFORE INSERT ON T_APROB_PRESUP
FOR EACH ROW
BEGIN
    SELECT T_APROB_PRESUP_AI.NEXTVAL
    INTO :new.ID_APROB_PRESUP
    FROM dual;    
END;