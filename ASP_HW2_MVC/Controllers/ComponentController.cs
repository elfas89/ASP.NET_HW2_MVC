using Homework2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP_HW2_MVC.Controllers
{
    public class ComponentController : Controller
    {
        // GET: Component
        public ActionResult Index()
        {
            //return View();

            //IDictionary<int, Figure> figuresDictionary;
            IDictionary<string, Component> componentList;

            if (Session["Components"] == null)
            {
                //в сессии пусто, создаем коллекцию, наполняем по умолчанию
                componentList = new Dictionary<string, Component>();
                componentList.Add("Sony", new TV("Sony"));
                componentList.Add("Aiwa", new MediaCenter("Aiwa", 88.8));
                componentList.Add("LG", new Fridge("LG"));

                Session["Components"] = componentList;
            }
            else
            {
                //не пусто, берем коллекцию из сессии
                componentList = (Dictionary<string, Component>)Session["Components"];
            }


            ViewBag.componentList = componentList;

            return View(componentList);

        }

        // GET: Component/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Component/Create
        public ActionResult Create()
        {
            SelectListItem[] dropDownComponentList = new SelectListItem[5];
            dropDownComponentList[0] = new SelectListItem { Text = "Телевизор", Value = "tv", Selected = true };
            dropDownComponentList[1] = new SelectListItem { Text = "Холодильник", Value = "fridge" };
            dropDownComponentList[2] = new SelectListItem { Text = "Печь", Value = "stove" };
            dropDownComponentList[3] = new SelectListItem { Text = "Духовка", Value = "oven" };
            dropDownComponentList[4] = new SelectListItem { Text = "Музыкальный центр", Value = "media" };

            ViewBag.dropDownComponentList = dropDownComponentList;

            return View();
        }

        // POST: Component/Create
        [HttpPost]
        public ActionResult Create(string componentType, string componentName)
        {
            // повторяем выпадающий список - для отображения
            SelectListItem[] dropDownComponentList = new SelectListItem[5];
            dropDownComponentList[0] = new SelectListItem { Text = "Телевизор", Value = "tv", Selected = true };
            dropDownComponentList[1] = new SelectListItem { Text = "Холодильник", Value = "fridge" };
            dropDownComponentList[2] = new SelectListItem { Text = "Печь", Value = "stove" };
            dropDownComponentList[3] = new SelectListItem { Text = "Духовка", Value = "oven" };
            dropDownComponentList[4] = new SelectListItem { Text = "Музыкальный центр", Value = "media" };

            ViewBag.dropDownComponentList = dropDownComponentList;


                // создаем коллекцию, заполняем из сессии
                IDictionary<string, Component> componentList = (Dictionary<string, Component>)Session["Components"];

                // проверка заполненности поля для имени
                // проверка наличия имени

                if (componentName == "")
                {
                    ViewBag.ErrorNoname = "Укажите имя компонента!";
                    return View();
                }
                else if (componentList.ContainsKey(componentName))
                {
                    ViewBag.ErrorContains = "Такое имя уже существует. Укажите другое имя компонента.";
                    return View();
                }
                else
                {
                   // определяем выбранный тип компонента, создаем объект
                    Component newComponent;
                    switch (componentType)
                    {
                        default:
                            newComponent = new TV(componentName);
                            break;
                        case "fridge":
                            newComponent = new Fridge(componentName);
                            break;
                        case "stove":
                            newComponent = new Stove(componentName);
                            break;
                        case "oven":
                            newComponent = new Oven(componentName, 0, 96);
                            break;
                        case "media":
                            newComponent = new MediaCenter(componentName, 88.8);
                            break;
                    }

                    // добавляем в коллекцию
                    componentList.Add(componentName, newComponent);

                // записываем в сессию
                Session["Components"] = componentList;

                // возвращаемся на главную
                return RedirectToAction("Index");
                }

        }

        // GET: Component/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Component/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(string name)
        {
            // создаем коллекцию, заполняем из сессии
            IDictionary<string, Component> componentList = (Dictionary<string, Component>)Session["Components"];

            // удаляем, записываем обратно в сессию
            componentList.Remove(name);
            Session["Components"] = componentList;

            return RedirectToAction("Index");
        }



        //public ActionResult SetSide(int id, double parametr)
        //{
        //    IDictionary<int, Figure> figuresDictionary = (SortedDictionary<int, Figure>)Session["Figures"];
        //    ((ISidable)figuresDictionary[id]).A = parametr;
        //    Session["Figures"] = figuresDictionary;

        //    return RedirectToAction("Index");
        //}

        //public ActionResult RadiusSet(int id, double parametr)
        //{
        //    IDictionary<int, Figure> figuresDictionary = (SortedDictionary<int, Figure>)Session["Figures"];
        //    ((IRadiusable)figuresDictionary[id]).R = parametr;
        //    Session["Figures"] = figuresDictionary;

        //    return RedirectToAction("Index");
        //}



    }
}
