<template>
  <b-container class="post-container">
    <ListPost :posts="getPosts" />
  </b-container>
</template>
<script>
import { mapMutations, mapGetters } from "vuex";
import blogType from "@/utils/blogType";
import ListPost from "@/components/Post/List";
export default {
  name: "Local",
  data() {
    return {
      posts: [],
    };
  },
  components: {
    ListPost,
  },
  computed: {
    ...mapGetters({
      getPosts: "local/getAllPosts",
    }),
  },
  mounted() {
    this.$store.commit("blog/SET_BLOG_TYPE", blogType.LOCAL);
  },
  methods: {
    ...mapMutations(["blog/SET_BLOG_TYPE"]),
  },
};
</script>
