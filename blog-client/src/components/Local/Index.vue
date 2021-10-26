<template>
  <b-container class="post-container">
    <ListPost />
  </b-container>
</template>
<script>
import { mapMutations } from "vuex";
import blogType from "@/utils/blogType";
import ListPost from "@/components/Post/List";
export default {
  name: "Local",
  components: {
    ListPost,
  },
  mounted() {
    this.$store.commit("blog/SET_BLOG_TYPE", blogType.LOCAL);
  },
  methods: {
    ...mapMutations(["blog/SET_BLOG_TYPE"]),
  },
};
</script>
