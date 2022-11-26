<template>
    <EmployeeList />
</template>

<script>
import EmployeeList from '../../views/employee/EmployeeList.vue'
export default {
    name:"TheMainContent",
    components:{EmployeeList},
    props:[],
    methods:{},
    data() {
        return {
        }
    },
}
</script>