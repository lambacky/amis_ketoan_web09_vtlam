import axios from "axios";
import CONST_API from "@/constants/api";


const actions = {
    /**
     * Lấy dữ liệu phòng ban
     * @param {*} context
     * Author: Vũ Tùng Lâm (30/10/2022)
     */
    async getDepartment(context) {
      try {
        const res = await axios.get(`${CONST_API}/Departments`);
        context.commit("getDepartment", res.data);
      } catch (error) {
        console.log(error);
      }
    },
};
export default actions