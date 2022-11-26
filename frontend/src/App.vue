<template>
    <TheSidebar/>
  <div class="main">
    <TheHeader/>
    <TheMainContent/>
  </div>
  <BaseLoading v-if="isShowLoading" />
  <BaseToastMessage v-if="isShowToastMessage" />
</template>

<script>
import {  mapGetters } from 'vuex'
import TheHeader from './components/layouts/TheHeader.vue'
import TheMainContent from './components/layouts/TheMainContent.vue'
import TheSidebar from './components/layouts/TheSidebar.vue'
import BaseLoading from './components/base/BaseLoading.vue'
import BaseToastMessage from './components/base/BaseToastMessage.vue'

export default {
  name: 'App',
  components: {
    TheHeader,TheSidebar,TheMainContent,BaseLoading,BaseToastMessage
  },
  computed: mapGetters(["isShowLoading","isShowToastMessage"])
}
</script>

<style>
@import url(./css/main.css);
</style>
