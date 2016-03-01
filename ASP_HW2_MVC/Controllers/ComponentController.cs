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
        // поле
        public IDictionary<string, Component> componentList;

        // конструктор
        public ComponentController()
        {
            if (System.Web.HttpContext.Current.Session["Components"] == null)
            {
                //в сессии пусто, создаем коллекцию, наполняем по умолчанию, записываем в сессию
                componentList = new Dictionary<string, Component>();
                    componentList.Add("Sony", new TV("Sony"));
                    componentList.Add("Aiwa", new MediaCenter("Aiwa", 88.8));
                    componentList.Add("LG", new Fridge("LG"));

                    System.Web.HttpContext.Current.Session["Components"] = componentList;
            }
            else
            {
                //не пусто, берем коллекцию из сессии
                componentList = (Dictionary<string, Component>)System.Web.HttpContext.Current.Session["Components"];
            }
        }

        // GET: Component
        public ActionResult Index()
        {

            //данные для вью
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
            ViewBag.dropDownComponentList = CreateComponentList();
            return View();
        }


        // POST: Component/Create
        [HttpPost]
        public ActionResult Create(string componentType, string componentName)
        {
            // повторяем выпадающий список - для отображения
            ViewBag.dropDownComponentList = CreateComponentList();

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

                // возвращаемся на главную
                return RedirectToAction("Index");
                }

        }

        // выпадающий список устройств
        private SelectListItem[] CreateComponentList()
        {
            SelectListItem[] dropDownComponentList = new SelectListItem[5];
            dropDownComponentList[0] = new SelectListItem { Text = "Телевизор", Value = "tv", Selected = true };
            dropDownComponentList[1] = new SelectListItem { Text = "Холодильник", Value = "fridge" };
            dropDownComponentList[2] = new SelectListItem { Text = "Печь", Value = "stove" };
            dropDownComponentList[3] = new SelectListItem { Text = "Духовка", Value = "oven" };
            dropDownComponentList[4] = new SelectListItem { Text = "Музыкальный центр", Value = "media" };

            return dropDownComponentList;
        }


        public ActionResult Delete(string name)
        {
            // удаляем
            componentList.Remove(name);
            return RedirectToAction("Index");
        }

        public ActionResult On(string name)
        {
            ((IPowerable)componentList[name]).PowerOn();
            return RedirectToAction("Index");
        }

        public ActionResult Off(string name)
        {
            ((IPowerable)componentList[name]).PowerOff();
            return RedirectToAction("Index");
        }

        public ActionResult Open(string name)
        {
            ((IOpenable)componentList[name]).Open();
            return RedirectToAction("Index");
        }

        public ActionResult Close(string name)
        {
            ((IOpenable)componentList[name]).Close();
            return RedirectToAction("Index");
        }

        public ActionResult PrevChannel(string name)
        {
            ((TV)componentList[name]).PrevChannel();
            return RedirectToAction("Index");
        }

        public ActionResult NextChannel(string name)
        {
            ((TV)componentList[name]).NextChannel();
            return RedirectToAction("Index");
        }

        // установка значения
        [HttpPost]
        public ActionResult Set(string name, string valueBox)
        {

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

            return RedirectToAction("Index");
        }


    }
}
