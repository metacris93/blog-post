<template>
  <b-container>
    <template v-for="post in posts">
      <Post
        :key="post.id"
        :id="post.id"
        :title="post.title"
        :content="post.content"
        :createdAt="post.createdAt"
        :updatedAt="post.updatedAt"
      />
    </template>
  </b-container>
</template>
<script>
import moment from "moment";
import Post from "./Index";
export default {
  name: "PostList",
  components: {
    Post,
  },
  data() {
    return {
      posts: [
        {
          id: 1,
          title: "Title 1",
          content:
            "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Similique veritatis ipsa unde nostrum! Quidem doloremque iste repudiandae libero, atque possimus mollitia quibusdam quam exercitationem reprehenderit, eligendi quis pariatur nemo natus?",
          createdAt: moment().subtract(1, "days").calendar(),
          updatedAt: moment().subtract(1, "days").calendar(),
          //image: null
        },
        {
          id: 2,
          title: "Title 2",
          content:
            "Lorem ipsum dolor sit amet consectetur adipisicing elit. Molestias suscipit nobis voluptatum consequuntur eveniet quaerat, explicabo perferendis incidunt eligendi neque! Mollitia id incidunt commodi aliquid beatae numquam veniam illo eum?",
          createdAt: moment().subtract(1, "days").calendar(),
          updatedAt: moment().subtract(1, "days").calendar(),
          //image: null
        },
        {
          id: 3,
          title: "Title 3",
          content:
            "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Beatae officia sapiente veritatis, sed esse odit consectetur! Neque assumenda eaque voluptatibus veritatis eveniet quam quisquam. Dolores ab quam dolorem ut iure.",
          createdAt: moment().subtract(1, "days").calendar(),
          updatedAt: moment().subtract(1, "days").calendar(),
          //image: null
        },
      ],
    };
  },
};
</script>
