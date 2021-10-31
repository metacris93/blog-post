<template>
  <div id="app">
    <Header />
    <MainLayout />
  </div>
</template>
<script>
import Header from "./components/Header/Index";
import MainLayout from "./layouts/MainLayout";
export default {
  name: "App",
  components: {
    Header,
    MainLayout,
  },
};
</script>
<style lang="stylus">
@import url("https://fonts.googleapis.com/css?family=Trirong");
#app
  font-family Avenir, Helvetica, Arial, sans-serif
  -webkit-font-smoothing antialiased
  -moz-osx-font-smoothing grayscale
  text-align center
  color #2c3e50
  position relative
</style>
