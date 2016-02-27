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


            SelectListItem[] modesList = new SelectListItem[3];
            modesList[0] = new SelectListItem { Text = "нормальный", Value = "normal", Selected = true };
            modesList[1] = new SelectListItem { Text = "северный", Value = "north" };
            modesList[2] = new SelectListItem { Text = "южный", Value = "south" };

            ViewBag.modesList = modesList;


            return View(componentList);

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


        public ActionResult Delete(string name)
        {
            // создаем коллекцию, заполняем из сессии
            IDictionary<string, Component> componentList = (Dictionary<string, Component>)Session["Components"];

            // удаляем, записываем обратно в сессию
            componentList.Remove(name);
            Session["Components"] = componentList;

            return RedirectToAction("Index");
        }

        public ActionResult On(string name)
        {
            IDictionary<string, Component> componentList = (Dictionary<string, Component>)Session["Components"];
            ((IPowerable)componentList[name]).PowerOn();

            Session["Components"] = componentList;

            return RedirectToAction("Index");
        }

        public ActionResult Off(string name)
        {
            IDictionary<string, Component> componentList = (Dictionary<string, Component>)Session["Components"];
            ((IPowerable)componentList[name]).PowerOff();

            Session["Components"] = componentList;

            return RedirectToAction("Index");
        }

        public ActionResult Open(string name)
        {
            IDictionary<string, Component> componentList = (Dictionary<string, Component>)Session["Components"];
            ((IOpenable)componentList[name]).Open();
            Session["Components"] = componentList;
            return RedirectToAction("Index");
        }

        public ActionResult Close(string name)
        {
            IDictionary<string, Component> componentList = (Dictionary<string, Component>)Session["Components"];
            ((IOpenable)componentList[name]).Close();
            Session["Components"] = componentList;
            return RedirectToAction("Index");
        }

        public ActionResult PrevChannel(string name)
        {
            IDictionary<string, Component> componentList = (Dictionary<string, Component>)Session["Components"];
            ((TV)componentList[name]).PrevChannel();
            Session["Components"] = componentList;
            return RedirectToAction("Index");
        }

        public ActionResult NextChannel(string name)
        {
            IDictionary<string, Component> componentList = (Dictionary<string, Component>)Session["Components"];
            ((TV)componentList[name]).NextChannel();
            Session["Components"] = componentList;
            return RedirectToAction("Index");
        }

        // установка значения
        [HttpPost]
        public ActionResult Set(string name, string valueBox)
        {
            IDictionary<string, Component> componentList = (Dictionary<string, Component>)Session["Components"];

            if (componentList[name] is MediaCenter)
            {
                try
                {
                    int v = Convert.ToInt32(valueBox);
                    ((MediaCenter)componentList[name]).SetVolume(v);
                }

                catch (Exception ex)
                {
                    ViewBag.NoInt = "Введите числовое значение";
                }

            }

            if (componentList[name] is Oven)
            {
                try
                {
                    int t = Convert.ToInt32(valueBox);
                    ((Oven)componentList[name]).SetTemper(t);
                }
                catch (Exception ex)
                {
                    ViewBag.NoInt = "Введите числовое значение";
                }
            }

            if (componentList[name] is Fridge)
            {
                //данные для вью
                SelectListItem[] modesList = new SelectListItem[3];
                modesList[0] = new SelectListItem { Text = "нормальный", Value = "normal", Selected = true };
                modesList[1] = new SelectListItem { Text = "северный", Value = "north" };
                modesList[2] = new SelectListItem { Text = "южный", Value = "south" };

                ViewBag.modesList = modesList;

                switch (valueBox)
                {
                    case ("normal"):
                        ((Fridge)componentList[name]).Normal();
                        break;
                    case ("north"):
                        ((Fridge)componentList[name]).North();
                        break;
                    case ("south"):
                        ((Fridge)componentList[name]).South();
                        break;
                    default:
                        break;
                }
            }

            Session["Components"] = componentList;
            return RedirectToAction("Index");
        }



    }
}
