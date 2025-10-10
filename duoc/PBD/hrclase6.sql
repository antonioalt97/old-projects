


select last_name , salary from employees where salary > (select salary from employees where last_name= 'Abel');

SELECT employee_id, job_id, salary
FROM employees
WHERE job_id = (SELECT job_id  FROM employees WHERE employee_id = 141);



SELECT employee_id, job_id, salary
FROM employees
WHERE job_id = 
                             (SELECT job_id
                              FROM employees
                              WHERE employee_id = 141)
AND salary >                 
                             (SELECT salary
                              FROM employees
                              WHERE employee_id = 143);


SELECT department_id, MIN(salary)
FROM     employees
GROUP BY department_id
HAVING MIN(salary) >         
                                       (SELECT MIN(salary)
                                           FROM employees
                                         WHERE department_id = 50)
ORDER BY MIN(salary);




select employee_id , concat(first_name, concat(' ',last_name)), job_id , to_char(salary,'$99999,999') from employees
where salary= (select salary from employees where employee_id = 204);


select employee_id EMPLEADO, to_char(salary,'$99999,999') "SALARIO ACTUAL" , hire_date "FECHA CONTRATO", 
round((sysdate-hire_date)) "DIAS TRABAJADOS", round(months_between(sysdate,hire_date)) "MESES TRABAJADOS",
round(months_between(sysdate,hire_date)/12) "AÑOS TRABAJADOS"
 from employees
 order by salary , employee_id asc;
 
select department_name DEPARTAMENTO, (count(employee_id)) from employees join departments using (department_id)
group by department_name
having (count(employee_id) = (select max(count(employee_id)) from employees group by department_id))

order by department_name;










