<template>
  <div id="app">
    <Header />
    <MainLayout />
    <router-link
      v-if="$route.name !== 'NewPost'"
      class="btn-circle btn-xl new-post"
      :to="{ name: 'NewPost' }"
    >
      <b-icon icon="plus-circle" scale="3" aria-hidden="true"></b-icon>
    </router-link>
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
.new-post
  position fixed
  top 20%
  left 95%
.btn-circle, .btn-xl
  width 70px
  height 70px
  padding 10px 16px
  border-radius 35px
  font-size 24px
  line-height 1.33
.new-post
  right 0
  top 90%
</style>
