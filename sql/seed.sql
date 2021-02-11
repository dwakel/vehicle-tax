--Vehicle Category

INSERT INTO public.vehicle_category (id, short_name, description) VALUES (1000, '', 'AMBULANCES & HEARSE');

INSERT INTO public.vehicle_category (id, short_name, description) VALUES (1001, '', 'MOTOR CARS INCLUDING CROSS COUNTRY AND ESTATE CARS:  DIESEL');

INSERT INTO public.vehicle_category (id, short_name, description) VALUES (1002, '', 'MOTOR CARS INCLUDING CROSS COUNTRY AND ESTATE CARS:  PETROL');

INSERT INTO public.vehicle_category (id, short_name, description) VALUES (1003, '', 'MOTOR VEHICLE FOR THE TRANSPORT OF GOODS SUCH AS TRUCKS, TIPPING TRUCKS,VANS, REFUSE TRUCK, TANKERS AND LORRIES');

INSERT INTO public.vehicle_category (id, short_name, description) VALUES (1004, '', 'OTHER VEHICLES');




--Vehicle TYPE
-------------

--Vehicle type of Category AMBULANCES & HEARSE
INSERT INTO public.vehicle_type (id, vehicle_category_id, short_name, description) VALUES (1100, 1000, 'Ambulance', 'Ambulance');
INSERT INTO public.vehicle_type (id, vehicle_category_id, short_name, description) VALUES (1101, 1000, 'Hearse', 'Hearse');


--Vehicle type of Category MOTOR CARS INCLUDING CROSS COUNTRY AND ESTATE CARS:  DIESEL
INSERT INTO public.vehicle_type (id, vehicle_category_id, short_name, description) VALUES (1102, 1001, '', 'Of a cylinder capacity not exceeding 1500 cc');
INSERT INTO public.vehicle_type (id, vehicle_category_id, short_name, description) VALUES (1103, 1001, '', 'Of a cylinder capacity exceeding 1500cc but not exceeding 2500cc');
INSERT INTO public.vehicle_type (id, vehicle_category_id, short_name, description) VALUES (1104, 1001, '', 'Of a cylinder capacity exceeding 2500cc');


--Vehicle type of Category MOTOR CARS INCLUDING CROSS COUNTRY AND ESTATE CARS:  PETROL
INSERT INTO public.vehicle_type (id, vehicle_category_id, short_name, description) VALUES (1105, 1002, '', 'Of a cylinder capacity not exceeding 1000 cc');
INSERT INTO public.vehicle_type (id, vehicle_category_id, short_name, description) VALUES (1106, 1002, '', 'Of a cylinder capacity exceeding 1000cc but not exceeding 3000cc');
INSERT INTO public.vehicle_type (id, vehicle_category_id, short_name, description) VALUES (1107, 1002, '', 'Of a cylinder capacity exceeding 3000cc');
INSERT INTO public.vehicle_type (id, vehicle_category_id, short_name, description) VALUES (1108, 1002, '', 'Motor Cars designed for travelling on snow; golf cars and similar vehicles');
INSERT INTO public.vehicle_type (id, vehicle_category_id, short_name, description) VALUES (1109, 1002, '', 'Motor Vehicles designed to carry ten (10) and more person');
INSERT INTO public.vehicle_type (id, vehicle_category_id, short_name, description) VALUES (1110, 1002, '', 'Motor Vehicles designed to carry thirty (30) and more person');




--Vehicle type of Category MOTOR VEHICLE FOR THE TRANSPORT OF GOODS SUCH AS TRUCKS, TIPPING TRUCKS,VANS, REFUSE TRUCK, TANKERS AND LORRIES
INSERT INTO public.vehicle_type (id, vehicle_category_id, short_name, description) VALUES (1111, 1003, '', 'Pedestrian Controlled tractors – 8701.10');
INSERT INTO public.vehicle_type (id, vehicle_category_id, short_name, description) VALUES (1112, 1003, '', 'Road tractors (Articulator Head) for semi-trailers-8701.20');
INSERT INTO public.vehicle_type (id, vehicle_category_id, short_name, description) VALUES (1113, 1003, '', 'Track –laying tractors-8701.30');
INSERT INTO public.vehicle_type (id, vehicle_category_id, short_name, description) VALUES (1114, 1003, '', 'Agricultural Tractor – 8701.91.90.00');
INSERT INTO public.vehicle_type (id, vehicle_category_id, short_name, description) VALUES (1115, 1003, '', 'Other tractors – 8701.90');
INSERT INTO public.vehicle_type (id, vehicle_category_id, short_name, description) VALUES (1116, 1003, '', 'Tipping, Flat,Container, Skeleton, Low bed or Low Loader Trailers 8716');
INSERT INTO public.vehicle_type (id, vehicle_category_id, short_name, description) VALUES (1117, 1003, '', 'Pick-ups 8704');
INSERT INTO public.vehicle_type (id, vehicle_category_id, short_name, description) VALUES (1118, 1003, '', 'Dumpers designed for off-highway use 8704.10.90.00');
INSERT INTO public.vehicle_type (id, vehicle_category_id, short_name, description) VALUES (1119, 1003, '', 'Special purpose motor vehicles of HS Code 87.05 (For example workshop vans, breakdown vehicle and mobile showrooms)');
INSERT INTO public.vehicle_type (id, vehicle_category_id, short_name, description) VALUES (1120, 1003, '', 'Off road terminal tractors 8701. Reference WCO: Annex A/X. Possible amendment to the compendium of classification opinions to reflect the decisions to classify a (TJ 5000 off road) vehicle and an (Ottawa 4×2 off road YT50) vehicle in heading 87.01 (Subheading 8701.90)');




--Vehicle type of Category OTHER VEHICLES
INSERT INTO public.vehicle_type (id, vehicle_category_id, short_name, description) VALUES (1121, 1004, '', 'Motor Cycle/Bikes 8711');
INSERT INTO public.vehicle_type (id, vehicle_category_id, short_name, description) VALUES (1122, 1004, '', 'Bicycle 8712');



--Vehicle tax

INSERT INTO public.vehicle_tax (vehicle_type_id, import_duty, vat, nhil, getfund_levy, au_levy, ecowas_levy, exim_levy, exam_levy,special_import_levy) VALUES (1100, .2, .125, .025, .025, .02, .05, .075, .01, .02);
INSERT INTO public.vehicle_tax (vehicle_type_id, import_duty, vat, nhil, getfund_levy, au_levy, ecowas_levy, exim_levy, exam_levy,special_import_levy) VALUES (1101, .2, .125, .025, .025, .02, .05, .075, .01, .02);

INSERT INTO public.vehicle_tax (vehicle_type_id, import_duty, vat, nhil, getfund_levy, au_levy, ecowas_levy, exim_levy, exam_levy,special_import_levy) VALUES (1102, .05, .125, .025, .025, .02, .05, .075, .01, .02);
INSERT INTO public.vehicle_tax (vehicle_type_id, import_duty, vat, nhil, getfund_levy, au_levy, ecowas_levy, exim_levy, exam_levy,special_import_levy) VALUES (1103, .1, .125, .025, .025, .02, .05, .075, .01, .02);
INSERT INTO public.vehicle_tax (vehicle_type_id, import_duty, vat, nhil, getfund_levy, au_levy, ecowas_levy, exim_levy, exam_levy,special_import_levy) VALUES (1104, .2, .125, .025, .025, .02, .05, .075, .01, .02);


INSERT INTO public.vehicle_tax (vehicle_type_id, import_duty, vat, nhil, getfund_levy, au_levy, ecowas_levy, exim_levy, exam_levy,special_import_levy) VALUES (1105, .05, .125, .025, .025, .02, .05, .075, .01, .02);
INSERT INTO public.vehicle_tax (vehicle_type_id, import_duty, vat, nhil, getfund_levy, au_levy, ecowas_levy, exim_levy, exam_levy,special_import_levy) VALUES (1106, .1, .125, .025, .025, .02, .05, .075, .01, .02);
INSERT INTO public.vehicle_tax (vehicle_type_id, import_duty, vat, nhil, getfund_levy, au_levy, ecowas_levy, exim_levy, exam_levy,special_import_levy) VALUES (1107, .2, .125, .025, .025, .02, .05, .075, .01, .02);
INSERT INTO public.vehicle_tax (vehicle_type_id, import_duty, vat, nhil, getfund_levy, au_levy, ecowas_levy, exim_levy, exam_levy,special_import_levy) VALUES (1108, .2, .125, .025, .025, .02, .05, .075, .01, .02);
INSERT INTO public.vehicle_tax (vehicle_type_id, import_duty, vat, nhil, getfund_levy, au_levy, ecowas_levy, exim_levy, exam_levy,special_import_levy) VALUES (1109, .05, .125, .025, .025, .02, .05, .075, .01, .02);
INSERT INTO public.vehicle_tax (vehicle_type_id, import_duty, vat, nhil, getfund_levy, au_levy, ecowas_levy, exim_levy, exam_levy,special_import_levy) VALUES (1110, .05, .125, .025, .025, .02, .05, .075, .01, .02);




INSERT INTO public.vehicle_tax (vehicle_type_id, import_duty, vat, nhil, getfund_levy, au_levy, ecowas_levy, exim_levy, exam_levy,special_import_levy) VALUES (1111, .05, 0, 0, 0, .02, .05, .075, .01, .02);
INSERT INTO public.vehicle_tax (vehicle_type_id, import_duty, vat, nhil, getfund_levy, au_levy, ecowas_levy, exim_levy, exam_levy,special_import_levy) VALUES (1112, .05, .125, .025, .025, .02, .05, .075, .01, .02);
INSERT INTO public.vehicle_tax (vehicle_type_id, import_duty, vat, nhil, getfund_levy, au_levy, ecowas_levy, exim_levy, exam_levy,special_import_levy) VALUES (1113, .05, 0, 0, 0, .02, .05, .075, .01, .02);
INSERT INTO public.vehicle_tax (vehicle_type_id, import_duty, vat, nhil, getfund_levy, au_levy, ecowas_levy, exim_levy, exam_levy,special_import_levy) VALUES (1114, .0, 0, 0, 0, .02, .05, .075, .01, .02);
INSERT INTO public.vehicle_tax (vehicle_type_id, import_duty, vat, nhil, getfund_levy, au_levy, ecowas_levy, exim_levy, exam_levy,special_import_levy) VALUES (1115, .05, 0, 0, 0, .02, .05, .075, .01, .02);
INSERT INTO public.vehicle_tax (vehicle_type_id, import_duty, vat, nhil, getfund_levy, au_levy, ecowas_levy, exim_levy, exam_levy,special_import_levy) VALUES (1116, .05, .125, .025, .025, .02, .05, .075, .01, .02);
INSERT INTO public.vehicle_tax (vehicle_type_id, import_duty, vat, nhil, getfund_levy, au_levy, ecowas_levy, exim_levy, exam_levy,special_import_levy) VALUES (1117, .1, .125, .025, .025, .02, .05, .075, .01, .02);
INSERT INTO public.vehicle_tax (vehicle_type_id, import_duty, vat, nhil, getfund_levy, au_levy, ecowas_levy, exim_levy, exam_levy,special_import_levy) VALUES (1118, .05, 0, 0, 0, .02, .05, .075, .01, .02);
INSERT INTO public.vehicle_tax (vehicle_type_id, import_duty, vat, nhil, getfund_levy, au_levy, ecowas_levy, exim_levy, exam_levy,special_import_levy) VALUES (1119, .05, .125, .025, .025, .02, .05, .075, .01, .02);
INSERT INTO public.vehicle_tax (vehicle_type_id, import_duty, vat, nhil, getfund_levy, au_levy, ecowas_levy, exim_levy, exam_levy,special_import_levy) VALUES (1120, .05, .125, .025, .025, .02, .05, .075, .01, .02);



INSERT INTO public.vehicle_tax (vehicle_type_id, import_duty, vat, nhil, getfund_levy, au_levy, ecowas_levy, exim_levy, exam_levy,special_import_levy) VALUES (1121, .2, .125, .025, .025, .02, .05, .075, .01, .02);
INSERT INTO public.vehicle_tax (vehicle_type_id, import_duty, vat, nhil, getfund_levy, au_levy, ecowas_levy, exim_levy, exam_levy,special_import_levy) VALUES (1122, .2, 0, 0, 0, .02, .05, .075, .01, .02);
