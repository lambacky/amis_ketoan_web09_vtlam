import { createStore } from "vuex";
import employee from "./modules/employee/index";
import department from "./modules/department/index";
import app from "./modules/app/index";

const store = createStore({
  modules: {
    app,
    employee,
    department
  },
});

export default store;
