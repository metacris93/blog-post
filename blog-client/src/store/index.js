import Vue from "vue";
import Vuex from "vuex";

import blog from "./modules/blog";
import local from "./modules/local";

Vue.use(Vuex);

export default new Vuex.Store({
  modules: {
    blog,
    local,
  },
  strict: process.env.NODE_ENV !== "production",
});
